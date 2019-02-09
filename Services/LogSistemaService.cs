using System;
using System.Collections.Generic;
using System.Linq;
using IpCorpTestApi.Models;
using IpCorpTestApi.DTOS;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace IpCorpTestApi.Services
{
    public interface ILogSistemaService
    {
        LogSistemaDTO GetByID(int ID);
        IList<LogSistemaDTO> GetAll();

        Task<bool> GetLogsFromSource(int batch);
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

        public LogSistemaDTO GetByID(int ID)
        {
            LogSistema lg = _context.LogsSistema.FirstOrDefault(x=>x.LogSistemaId==ID);
            if(lg==null)
                return null;
            
            return _mapper.Map<LogSistemaDTO>(lg);
        }

        public IList<LogSistemaDTO> GetAll()
        {
            IList<LogSistema> list = _context.LogsSistema.ToList();
            return _mapper.Map<IList<LogSistemaDTO>>(list);
        }  

        public async Task<bool> GetLogsFromSource(int batch)
        {   
            IConfigurationSection appSettingsSection = _configuration.GetSection("AppSettings");
            AppSettings appSettings = appSettingsSection.Get<AppSettings>();
            IpCorpApiClient ac = new IpCorpApiClient(appSettings.Service,appSettings.TokenService,appSettings.GrantType,appSettings.Username,appSettings.Password,appSettings.ClientID);            
            List<LogSistemaDTO> ls =await ac.GetLogs("$filter=Data ge 2019-02-08&$orderby=Data desc&$pagesize=2&$page=1");
            _context.LogsSistema.AddRange(_mapper.Map<IList<LogSistema>>(ls));
            _context.SaveChanges();
            return true;
        }

      
              
    }
}