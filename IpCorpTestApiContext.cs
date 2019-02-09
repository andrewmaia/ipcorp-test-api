using Microsoft.EntityFrameworkCore;
using IpCorpTestApi.Models;
using IpCorpTestApi.EntityTypesConfiguration;

namespace IpCorpTestApi
{
    public class IpCorpTestApiContext : DbContext
    {
        public IpCorpTestApiContext(DbContextOptions<IpCorpTestApiContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogSistemaConfiguration());                 
        }        

        public DbSet<LogSistema> LogsSistema { get; set; }  
    }        
}