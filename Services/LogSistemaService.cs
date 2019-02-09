using System;
using System.Collections.Generic;
using System.Linq;
using IpCorpTestApi.Models;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using IpCorpTestApi.Responses;

namespace IpCorpTestApi.Services
{
    public interface ILogSistemaService
    {
        LogSistema GetByID(int ID);
        IList<LogSistema> GetAll();

         Task<int> GetLogsFromSource(int batch);
    }
 
    public class LogSistemaService : ILogSistemaService
    {
        private readonly IpCorpTestApiContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;        
        public LogSistemaService(IpCorpTestApiContext context,IMapper mapper,IConfiguration configuration)
        {
             _context = context;
             _mapper = mapper;
             _configuration=configuration;           
        }        

        public LogSistema GetByID(int ID)
        {
            LogSistema ls = _context.LogsSistema.FirstOrDefault(x=>x.LogSistemaId==ID);
            if(ls==null)
                return null;
            
            return ls;
        }

        public IList<LogSistema> GetAll()
        {
            return _context.LogsSistema.ToList();
        }  

        public async Task<int> GetLogsFromSource(int batch)
        {   
            int  importedLogs =0;
            IConfigurationSection appSettingsSection = _configuration.GetSection("AppSettings");
            AppSettings appSettings = appSettingsSection.Get<AppSettings>();
            IpCorpApiClient ac = new IpCorpApiClient(appSettings.Service,appSettings.TokenService,appSettings.GrantType,appSettings.Username,appSettings.Password,appSettings.ClientID);            
            List<LogSistemaResponse> list =await ac.GetLogs(GetFilter(batch));

            foreach(LogSistemaResponse lsr in list)
            {
                if(!_context.LogsSistema.Any(x=>x.LogSistemaId==lsr.LogSistemaId))
                {
                    _context.LogsSistema.Add(_mapper.Map<LogSistema>(lsr));
                    importedLogs++;
                }                 
            }

            if(importedLogs>0)
                _context.SaveChanges();

            return importedLogs;
        }

        private string GetFilter(int batch)
        {
            string filter = "$filter=Data ge {date}&$orderby=Data desc&$pagesize={batch}&$page=1";
            filter = filter.Replace("{date}",DateTime.Today.ToString("yyyy-MM-dd"));
            filter = filter.Replace("{batch}",batch.ToString());
            return filter;
        }
      
              
    }
}