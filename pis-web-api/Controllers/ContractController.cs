using Microsoft.AspNetCore.Mvc;
using pis.Repositorys;
using pis.Services;
using pis_web_api.Models.db;
using pis_web_api.Models.post;
using pis_web_api.Services;

namespace pis_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : Controller
    {
        private readonly ILogger<ContractController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private ContractService _contractService;
        private VaccinePriceListRepository _vaccinePriceListRepository;
        private AnimalService _animalService;
        private readonly JournalService _journalService;

        public ContractController(ILogger<ContractController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _contractService = new ContractService();
            _vaccinePriceListRepository = new VaccinePriceListRepository();
            _animalService = new AnimalService();
            _journalService = new JournalService();
        }

        [HttpPost("opensRegister")]
        public IActionResult OpensRegister([FromBody]UserPost user, DateOnly startDateFilter, DateOnly endDateFilter, string filterValue = "", string filterField = "",
            string sortBy = nameof(Contract.Customer), bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            List<Contract> contracts;
            int totalItems;

            if(user.Roles.Intersect(new List<int>() {  1, 4, 6 }).Count() != 0)
            {
                (contracts, totalItems) = _contractService.GetContracts(startDateFilter, endDateFilter, filterValue, filterField,
                sortBy, isAscending, pageNumber, pageSize);
            }
            else if(user.Roles.Intersect(new List<int>() { 3, 2, 8, 7, 9, 11, 10 }).Count() != 0)
            {
                (contracts, totalItems) = _contractService.GetContractsByOrg(startDateFilter, endDateFilter, filterValue, filterField,
                sortBy, isAscending, pageNumber, pageSize, user);
            }
            else
            {
                return Forbid();
            }


            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = new
            {
                StartDateFilter = startDateFilter,
                EndDateFilter = endDateFilter,
                FilterValue = filterValue,
                FilterField = filterField,
                SortBy = sortBy,
                IsAscending = isAscending,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Contracts = contracts
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _contractService.GetEntry(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("addEntry")]
        public IActionResult AddEntry([FromBody] ContractPost conPost, int userId)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    if(conPost.ConclusionDate > conPost.ExpirationDate)
                    {
                        return BadRequest("Дата заключения больше даты окончания");
                    }
                    if(conPost.CustomerId == conPost.PerformerId)
                    {
                        return BadRequest("Заказчик и исполнитель не могут быть одинаковыми");
                    }
                    var con = conPost.ConvertToContract();
                    bool status = _contractService.AddEntry(con);

                    if (status)
                    {
                        _journalService.JournalAddContract(userId, con.IdContract);
                        return Ok(con.IdContract);
                    }
                    else
                    {
                        return BadRequest("Failed to add contract entry.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return BadRequest();
        }

        [HttpPost("deleteEntry/{id}")]
        public IActionResult DeleteEntry(int id, int userId)
        {
            var status = _contractService.DeleteEntry(id);

            if (status)
            {
                _journalService.JournalDeleteContract(userId, id);
                return Ok($"Contract with ID {id} has been deleted.");
            }
            else
            {
                return BadRequest($"Failed to delete organisation entry with ID {id}");
            }
        }

        [HttpPost("changeEntry/{id}")]
        public IActionResult ChangeEntry(int id, [FromBody] ContractPost conPost, int userId)
        {
            if (ModelState.IsValid)
            {
                var con = conPost.ConvertToContractWithId(id);

                bool status = _contractService.ChangeEntry(con);

                if (status)
                {
                    _journalService.JournalEditContract(userId,con.IdContract);
                    return Ok();
                }
                else
                {
                    return BadRequest("Failed to update organisation entry."); 
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("GetCurrentContractsByAnimalForVaccinations/{animalId}")]
        public IActionResult GetCurrentContractsByUser(int animalId, [FromBody]UserPost user)
        {
            if (ModelState.IsValid)
            {
                var (contracts, count) = _contractService.GetContractsByOrg(DateOnly.FromDateTime(DateTime.Today),
                    DateOnly.MaxValue, "", "", nameof(Contract.Customer), true, 1, 1000, user);
                var animal = _animalService.GetEntry(animalId);
                var resultContracts = _vaccinePriceListRepository.GetContractsByLocality(animal.LocalityId, contracts.Select(x => x.IdContract));
                return Ok(resultContracts);
            }

            return BadRequest(ModelState);
        }
    }
}
