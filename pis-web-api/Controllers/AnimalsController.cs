using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NUnit.Framework;
using pis.Repositorys;
using pis.Services;
using pis_web_api.Models.db;
using pis_web_api.Models.get;
using pis_web_api.Models.post;
using pis_web_api.References;
using pis_web_api.Repositorys;
using pis_web_api.Services;

namespace pis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly ILogger<AnimalController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private AnimalService animalService;
        private JournalsService<Animal> journalService;
        private UserService userService;
        private RoleService roleService;

        private static List<Role> openFullAccess = new List<Role>() 
        {
            RolesReferences.KURATOR_VETSERVICE,
            RolesReferences.KURATOR_TRAPPING,
            RolesReferences.KURATOR_SHELTER,
            RolesReferences.OPERATOR_VETSERVICE,
            RolesReferences.OPERATOR_TRAPPING,
            RolesReferences.SIGNER_VETSERVICE,
            RolesReferences.SIGNER_TRAPPING,
            RolesReferences.SIGNER_SHELTER,
            RolesReferences.OPERATOR_SHELTER,
            RolesReferences.DOCTOR,
            RolesReferences.DOCTOR_SHELTER,
            RolesReferences.ADMIN
        };

        private static List<Role> openAccesByOrg = new List<Role>()
        {
            RolesReferences.KURATOR_OMSU,
            RolesReferences.OPERATOR_OMSU,
            RolesReferences.SIGNER_OMSU
        };

        private static List<Role> editAccess = new List<Role>()
        {
            RolesReferences.OPERATOR_SHELTER,
            RolesReferences.DOCTOR,
            RolesReferences.DOCTOR_SHELTER,
            RolesReferences.ADMIN
        };
        public AnimalController(ILogger<AnimalController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            animalService = new AnimalService();
            journalService = new JournalsService<Animal>();
            userService = new UserService();
            roleService = new RoleService();
        }

        [HttpPost("OpensRegister")]
        public IActionResult OpensRegister([FromBody]UserPost user, string filterField = "", string filterValue = "", string sortBy = nameof(Animal.AnimalName), bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            List<Animal> animals;
            int totalItems;

            if (roleService.IsUserHasRole(user, openFullAccess))
            {
                (animals, totalItems) = animalService.GetAnimals(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
            }
            else if (roleService.IsUserHasRole(user, openAccesByOrg))
            {
                (animals, totalItems) = animalService.GetAnimalsByOrg(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize, user);
            }
            else
            {
                return Forbid();
            }

            animalService.CheckStatus(animals);
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            
            var result = new
            {
                FilterValue = filterValue,
                FilterField = filterField,
                SortBy = sortBy,
                IsAscending = isAscending,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Animals = animals
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetAnimal(int id)
        {
            var animal = animalService.GetEntry(id);

            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }

        [HttpPost("AddEntry")]
        public IActionResult AddEntry([FromBody] AnimalPost animalPost, int userId)
        {
            var user = userService.GetEntry(userId);
            var userGet = user.ConvertToUserGet();
            if (roleService.IsUserHasRole(userGet, editAccess))
            {
                if (ModelState.IsValid)
                {
                    var animal = animalPost.ConvertToAnimal();
                    bool status = animalService.AddEntry(animal);

                    if (status)
                    {
                        var fullAnimal = animalService.GetAnimal(animal.Id);
                        journalService.JournalCreate(userId, fullAnimal, JournalActionType.Добавить);
                        return Ok(animal.RegistrationNumber);
                    }
                    else
                    {
                        return BadRequest("Failed to add animal entry.");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Forbid("Недостаточно прав!");
            }
        }

        [HttpPost("DeleteEntry/{id}")]
        public IActionResult DeleteEntry(int id, int userId)
        {
            var user = userService.GetEntry(userId);
            var userGet = user.ConvertToUserGet();

            if(roleService.IsUserHasRole(userGet, editAccess))
            {
                var animal = animalService.GetAnimal(userId);
                journalService.JournalCreate(userId, animal, JournalActionType.Удалить);
                var status = animalService.DeleteEntry(id);

                if (status)
                {
                    return Ok($"Animal with ID {id} has been deleted.");
                }
                else
                {
                    return BadRequest($"Failed to delete animal entry with ID {id}");
                }
            }
            else
            {
                return Forbid("Недостаточно прав!");
            }
        }


        [HttpPost("ChangeEntry/{id}")]
        public IActionResult ChangeEntry(int id, [FromBody] AnimalPost animalPost, int userId)
        {
            var user = userService.GetEntry(userId);
            var userGet = user.ConvertToUserGet();
            if(roleService.IsUserHasRole(userGet, editAccess))
            {
                if (ModelState.IsValid)
                {
                    var animal = animalPost.ConvertToAnimalWithId(id);
                    bool status = animalService.ChangeEntry(animal);

                    if (status)
                    {
                        var fullAnimal = animalService.GetAnimal(animal.Id);
                        journalService.JournalCreate(userId, fullAnimal, JournalActionType.Изменить);
                        return Ok($"Animal with ID {id} has been updated.");
                    }
                    else
                    {
                        return BadRequest("Failed to update animal entry.");
                    }
                }

                return BadRequest(ModelState);
            }
            else
            {
                return Forbid("Недостаточно прав!");
            }
        }
    }
}