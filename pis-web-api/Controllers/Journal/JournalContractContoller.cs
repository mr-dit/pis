using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pis_web_api.Models.db;
using pis_web_api.Models.post;
using pis_web_api.Models.get;
using pis_web_api.Services;

namespace pis_web_api.Controllers.Journals
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalContractContolller : AbstractJournalController<Contract>
    {
    }
}
