using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IpCorpTestApi.DTOS;
using IpCorpTestApi.Models;
using IpCorpTestApi;
using IpCorpTestApi.Services;
using Microsoft.Extensions.Configuration;


namespace IpCorpTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogSistemaController : ControllerBase
    {


        private readonly ILogSistemaService _logSistemaService;
  

        public LogSistemaController(ILogSistemaService logSistemaService)
        {
            _logSistemaService = logSistemaService;
          
        }

        [HttpGet("{id}")]
        public ActionResult<LogSistemaDTO> Get(int id)
        {
            LogSistemaDTO ls= _logSistemaService.GetByID(id);
            if (ls==null)
                return NotFound();

            return ls;
        }


        [HttpGet()]
         public IList<LogSistemaDTO> GetAll()
        {

            return _logSistemaService.GetAll();
        } 

        [HttpGet("GetLogsFromSource")]
         public async Task<IActionResult> GetLogsFromSource()
        {
            await _logSistemaService.GetLogsFromSource(2);
            return Ok();
        }                 

    }
}
