using Microsoft.AspNetCore.Mvc;
using pis_web_api.Models.db;

namespace pis_web_api.Controllers.Journals
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalVaccinationsController : AbstractJournalController<Vaccination>
    {

    }
}
