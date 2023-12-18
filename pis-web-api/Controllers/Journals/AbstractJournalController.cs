using Microsoft.AspNetCore.Mvc;
using pis_web_api.Models.db;
using pis_web_api.Models.post;
using pis_web_api.References;
using pis_web_api.Services;

namespace pis_web_api.Controllers.Journals
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class AbstractJournalController<T> : ControllerBase where T : class, IJurnable
    {
        private JournalsService<T> _journalService;
        private RoleService _roleService;
        public AbstractJournalController()
        {
            _journalService = new JournalsService<T>();
            _roleService = new RoleService();
        }

        [HttpPost("openJournal")]
        public IActionResult OpenJournal([FromBody] UserPost user, string filterValue = "", string filterField = "", int pageNumber = 1, int pageSize = 10)
        {
            if (_roleService.IsUserHasRole(user, new List<Role>() { RolesReferences.ADMIN }))
            {
                var (journals, count) = _journalService.GetJournals(filterValue, filterField, pageNumber, pageSize, T.TableName);
                var converter = new JournalConverter();
                var journalsGet = converter.ToGet(journals);

                var totalPages = (int)Math.Ceiling((double)count / pageSize);

                var result = new
                {
                    FilterValue = filterValue,
                    FilterField = filterField,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalItems = count,
                    TotalPages = totalPages,
                    Journals = journalsGet
                };

                return Ok(result);
            }
            else
            {
                return Forbid("Недостаточно прав");
            }
        }

        [HttpPost("deleteJournals")]
        public IActionResult Delete([FromBody] int[] ids)
        {
            try
            {
                _journalService.Delete(ids);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}