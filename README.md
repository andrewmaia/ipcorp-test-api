

# Banco de Dados
Foi utilizado um banco de dados MySql. Não é necessário se preocupar com ele pois o mesmo está na LocaWeb e aplicação já está apontando para ele. De qualquer forma um arquivo com o script de criação do banco e das tabelas está junto a solução: DataBaseCreation.sql 

Como não tinha a informação de quais informação poderiam vir ou não da API todos os campos da tabela LogSistemaforam criados aceitando nulo, exceto o campo LogSistemaId

#Pacote AutoMapper
Foi utilizado a biblioteca AutoMapper para mapeamentos entre modelos e DTOS . Pode ser importante não expor alguns campos de um determinado modelo, por isso a utilização de um objeto secudário para expor apenas as propriedades necessárias. No que foi desenvolvido, o mapper está apenas passando dados de um objeto para o outro integralmente apenas para demonstrar sua utilização do Mapper.