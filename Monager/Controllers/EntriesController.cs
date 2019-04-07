using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monager.Database;
using Monager.DTOs;
using Monager.Services;

namespace Monager.Controllers
{
    [Route("api/entries")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly IEntriesService _entriesService;

        public EntriesController(IEntriesService entriesService)
        {
            _entriesService = entriesService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var entries = await _entriesService.GetEntries();
            return Ok(entries.OrderByDescending(e => e.Timestamp).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]EntryDto entry)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _entriesService.CreateEntry(entry));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update([FromBody]EntryDto entry)
        {
            return Ok(await _entriesService.UpdateEntry(entry));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete([FromRoute]int id)
        {
            await _entriesService.DeleteEntry(id);
            return Ok();
        }
    }
}
