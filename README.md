

# Banco de Dados
Foi utilizado um banco de dados MySql. Não é necessário se preocupar com ele pois o mesmo está na LocaWeb e aplicação já está apontando para ele. De qualquer forma um arquivo com o script de criação do banco e das tabelas está junto a solução: DataBaseCreation.sql 

Como não tinha a informação de quais informação poderiam vir ou não da API todos os campos da tabela LogSistemaforam criados aceitando nulo, exceto o campo LogSistemaId

#Pacote AutoMapper
Foi utilizado a biblioteca AutoMapper para mapeamentos entre o objeto response LogSistemaResponse e o modelo LogSistema. No que foi desenvolvido, o mapper está apenas passando dados de um objeto para o outro integralmente apenas para demonstrar sua utilização, mas em outros casos pode ser que os número de campos ou nomes não sejam iguais entre os objetos.


Adicionar itens Readme
-Questão do ApiClient não estar generico
-Questão de criar um token varias vezes
-Descricao rotina de importação: só grava um id uma vez e só pega os x ultimos
-AppSettings
-EntityFrameWork/Lazy Load
-Injecao: Implementação deve estar em outro projeto


***Verificar Readme e Email Encurtador URL