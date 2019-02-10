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

         Task<int> GetLogsFromSource();
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
            return _context.LogsSistema.OrderByDescending(x=>x.LogSistemaId).ToList();
        }  

        public async Task<int> GetLogsFromSource()
       {   
            IConfigurationSection appSettingsSection = _configuration.GetSection("AppSettings");
            AppSettings appSettings = appSettingsSection.Get<AppSettings>();

            //Acessa API IpCorp
            IpCorpApiClient ac = new IpCorpApiClient(appSettings.Service,appSettings.TokenService,appSettings.GrantType,appSettings.Username,appSettings.Password,appSettings.ClientID);            
            List<LogSistemaResponse> list =await ac.GetLogs(GetFilter(appSettings.Batch));
            
            //Remove todas as Logs ja importadas
            _context.RemoveRange(_context.LogsSistema);
            //Insere as novas Logs
            _context.LogsSistema.AddRange(_mapper.Map<IList<LogSistema>>(list));
            
            _context.SaveChanges();
            return list.Count;
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