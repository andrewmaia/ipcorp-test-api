CREATE DATABASE `ipcorptest` /*!40100 DEFAULT CHARACTER SET latin1 COLLATE latin1_general_ci */;

use ipcorptest;

CREATE TABLE `LogSistema` (
  `LogSistemaId` int(11) NOT NULL ,
  `Data` DATETIME  NULL,
  `Origem` varchar(1000) COLLATE latin1_general_ci NULL,
  `Contexto` varchar(1000) COLLATE latin1_general_ci NULL,
  `Severidade` varchar(1000) COLLATE latin1_general_ci NULL,
  `Mensagem` varchar(1000) COLLATE latin1_general_ci NULL,
  `ArquivoFonte` varchar(1000) COLLATE latin1_general_ci NULL,
  `MetodoFonte` varchar(1000) COLLATE latin1_general_ci NULL,
  `Maquina` varchar(1000) COLLATE latin1_general_ci NULL,
  `LinhaFonte` int(11) NULL,
  `Propriedades` varchar(1000) COLLATE latin1_general_ci NULL,
  `Excecao` varchar(1000) COLLATE latin1_general_ci NULL,
  `OrigemId` int(11) NULL,  
  `LogContextoId` int(11) NULL, 
  `UsuarioId` int(11) NULL, 
  `NomeUsuario` varchar(1000) COLLATE latin1_general_ci NULL,  
  PRIMARY KEY (`LogSistemaId`),
  UNIQUE KEY `ID_UNIQUE` (`LogSistemaId`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;
