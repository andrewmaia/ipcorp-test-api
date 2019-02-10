# ipcorp-test-api

Este projeto está no GIT em https://github.com/andrewmaia/ipcorp-test-api. 

# Rodar o projeto em desenvolvimento
Rodar a API pelo Visual Studio Code (Pressionar F5)
A API deve rodar com o endereço https://localhost:5001/
Todas os endpoints estão com o padrão de endereço api/controller

# Banco de Dados
Foi utilizado um banco de dados MySql. Não é necessário se preocupar com ele pois o mesmo está na LocaWeb e aplicação já está apontando para ele. De qualquer forma um arquivo com o script de criação do banco e das tabelas está junto a solução: DataBaseCreation.sql.
Como não tinha a informação de quais informação poderiam vir ou não da API todos os campos da tabela LogSistema foram criados aceitando nulo, exceto o campo LogSistemaId.

# Endpoints
Abaixo a lista dos Endpoints que API disponibiliza:

https://localhost:5001/Api/Logsistema/:id : Retorna a log com o campo LogSistemaId igual ao ID informado (Provem do bando MySql não diretamente da API da Ipcorp)

https://localhost:5001/Api/Logsistema : Retorna todas as logs disponíveis no sistema (Provem do bando MySql não diretamente da API da Ipcorp). Serão sempre 10 por questão de performance

https://localhost:5001/Api/Logsistema/GetLogsFromSource : Este endpoint é o resposável por executar a importação das últimas 10 logs da Api IpCorp para o banco de dados MySql. O número de registros a serem importados está armazenado no  arquivo appsettings.json, chave Batch e está como 10 mas pode ser alterado como quiserem. Antes de inserir as novas logs, a tabela de logs é sempre limpa para efeito de performance e ficar sempre com o número de registros configurado. Após a execução este endpoint retorna o número de registros importados (no caso 10).

# Arquitetura

Os endpoints acessam os controllers (Pasta Controllers) que por sua vez por injeção de depêndencia acessam o serviço responsável por executar a atividade em questão. Não estou usando nennhum framework de injeção de dependência, a injeção é feita manualmente no arquivo Startup.cs. O correto seria que a implementação dos serviços estivessem em um outro projeto separado das interfaces do serviços, mas foi feito dessa forma por questões de tempo.

Os serviços (Pasta Services) são responsáveis por implementar todas as regras do sistema. Apenas os serviços acessam o bando dados através do EntityFrameWork (Pacote Pomelo.EntityFrameworkCore.MySql).

O EntityFrameWork está configurado no modo lazy para evitar carregamentos desnecessários de dados (Microsoft.EntityFrameworkCore.Proxies).

Foi criada uma classe chamada IpCorpApiClient para acessar a Api da IpCorp. Foi utilizada a classe HttpClient e seus métodos assincronos para acessar os endpoint da API. Os endereços do serviço, assim como os parâmetros para gerar o token estão armazenados no arquivo appsettings.json e foram fortemente tipados na classe AppSettings.cs.
Observação de Melhoria: A cada chamada do serviço de logs é gerado um novo token, sendo que o mesmo token poderia ser utilizado várias vezes até que o mesmo expire.

Foi utilizado a biblioteca AutoMapper para mapeamentos entre o objeto response LogSistemaResponse e o modelo LogSistema. No que foi desenvolvido, o mapper está apenas passando dados de um objeto para o outro integralmente apenas para demonstrar sua utilização, mas em outros casos pode ser que os número de campos ou nomes não sejam iguais entre os objetos. O Automapper facilita este trabalho.



