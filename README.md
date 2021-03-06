# Pré-requisitos

* .NET Core 2.2
* VS Code
* Extensão C# no VS Code
* Instalar, via NuGet, o BCrypt
* SQL Server (Express+)
* SSMS
* Git
* Recomendado: Postman

# Sobre o projeto

* Restful API
* Stack: .NET Core
* Design pattern: SOLID
* Banco de dados: SQL Server
* ORM: Entity Framework
* CORS: Desabilitado (acesso global)
* HTTPS: Desabilitado
* Autenticação: JWT (Bearer Token)
* Senha de acesso de qualquer usuário: `123456`
* Collection do postman: `./__POSTMAN__`
* Backup do banco: `./__DB__`
* Swagger: `http://localhost:5000/swagger`

# Antes de começar

* Realizar restore do backup do banco de dados ("./__DB__/barbequeue.bacpac")
* Caso o banco esteja vazio, inserir estes usuários para teste:

> `insert into users(Username, Password)
values ('andre', '$2b$12$srrQ9ybGftCfmx7ejDynSu3pvvknkNB9fMJsAdSjgnOkWnZV/FwwK');`

> `insert into users(Username, Password)
values ('prova', '$2b$12$srrQ9ybGftCfmx7ejDynSu3pvvknkNB9fMJsAdSjgnOkWnZV/FwwK');`

* No arquivo `Startup.cs`, foi comentado uma parte do algoritmo que faz a geração automática do banco de dados através do Entity. Caso não queira fazer um import do .bacpac, pode-se utilizar esta função (linha 110).

# Rodando o projeto

* `dotnet build`
* `dotnet run`
