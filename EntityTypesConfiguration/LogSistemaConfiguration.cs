using Microsoft.EntityFrameworkCore;
using IpCorpTestApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IpCorpTestApi.EntityTypesConfiguration {
  public class LogSistemaConfiguration : IEntityTypeConfiguration<LogSistema> {

    public void Configure(EntityTypeBuilder<LogSistema> builder){
        builder.ToTable("LogSistema").HasKey(l => l.LogSistemaId);
                     
    }
  }
}
