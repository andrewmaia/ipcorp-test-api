using System;
using System.Runtime.Serialization;

namespace IpCorpTestApi.DTOS
{
    [DataContract]
    public class LogSistemaDTO
    {
        [DataMember(Name="LogSistemaId")]
        public int LogSistemaId { get; set; }

        [DataMember(Name="Data")]        
        public DateTime? Data { get; set; }

        [DataMember(Name="Origem")]        
        public string Origem { get; set; } 

        [DataMember(Name="Contexto")]        
        public string Contexto { get; set; } 

        [DataMember(Name="Severidade")]        
        public string Severidade { get; set; } 

        [DataMember(Name="Mensagem")]        
        public string Mensagem { get; set; }

        [DataMember(Name="ArquivoFonte")]        
        public string ArquivoFonte { get; set; } 

        [DataMember(Name="MetodoFonte")]        
        public string MetodoFonte { get; set; }

        [DataMember(Name="Maquina")]        
        public string Maquina { get; set; }

        [DataMember(Name="LinhaFonte")]        
        public int? LinhaFonte { get; set; }

        [DataMember(Name="Propriedades")]        
        public string Propriedades { get; set; } 

        [DataMember(Name="Excecao")]        
        public string Excecao { get; set; }

        [DataMember(Name="OrigemId")]        
        public int? OrigemId { get; set; } 

        [DataMember(Name="LogContextoId")]        
        public int? LogContextoId { get; set; }

        [DataMember(Name="UsuarioId")]        
        public int? UsuarioId { get; set; } 

        [DataMember(Name="NomeUsuario")]        
        public string NomeUsuario { get; set; }
          
    }
}
