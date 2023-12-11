﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pis_web_api.Models.db;
using pis_web_api.Models.post;
using pis_web_api.Models.get;
using pis_web_api.Services;

namespace pis_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalOrganisationContolller : ControllerBase
    {
        private JournalService _journalService;
        public JournalOrganisationContolller()
        {
            _journalService = new JournalService();
        }

        [HttpGet("openJournal")]
        public IActionResult OpenJournal([FromBody] UserPost user, string filterValue = "", string filterField = "", int pageNumber = 1, int pageSize = 10) 
        {
            var (journals, count) = _journalService.GetJournals(filterValue, filterField, pageNumber, pageSize, TableNames.Организации);
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
