using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IpCorpTestApi.DTOS;
using IpCorpTestApi.Models;
using IpCorpTestApi;
using IpCorpTestApi.Services;
using Microsoft.Extensions.Configuration;

namespace IpCorpTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {



        private readonly ILogSistemaService _logSistemaService;
        private readonly IConfiguration _configuration; 
        public ValuesController(ILogSistemaService logSistemaService,IConfiguration configuration)
        {
            _logSistemaService = logSistemaService;
             _configuration=configuration;            
        }

        // GET api/values
        [HttpGet]
        public async Task<List<LogSistemaDTO>> Get()
        {
            IConfigurationSection appSettingsSection = _configuration.GetSection("AppSettings");
            AppSettings appSettings = appSettingsSection.Get<AppSettings>();            
            IpCorpApiClient ac = new IpCorpApiClient(appSettings.Service,appSettings.TokenService,appSettings.GrantType,appSettings.Username,appSettings.Password,appSettings.ClientID);
            return await ac.GetLogs("$filter=Data ge 2019-02-08&$orderby=Data desc&$pagesize=2&$page=1");

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<LogSistemaDTO> Get(int id)
        {
            return _logSistemaService.GetByID(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
