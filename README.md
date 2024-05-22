# Acelera Dev - ToDo List

Esta aplicação serve para gerenciar uma lista de tarefas, relacionando-as a seus respectivos usuários.

Este projeto é um modelo de aprendizagem do Projeto Acelera Dev, do Grupo Carrefour Brasil.

![Logo do Grupo Carrefour Brasil](https://media.licdn.com/dms/image/D4D0BAQGrE_UnFL8plQ/company-logo_200_200/0/1708908772188/grupocarrefourbrasil_logo?e=1723680000&v=beta&t=s8_oIbxqF4K8COSGT4kCYgzU0YLA9u0mKqZForzdB0I)

## Objetivo

Este projeto tem como objetivo fornecer uma API RESTful para gerenciamento de tarefas, permitindo aos usuários criar, listar, atualizar e deletar tarefas e suas sub-tarefas. É uma aplicação desenvolvida com.NET Core, utilizando o Entity Framework Core para persistência de dados.

## Pontos fortes

| Descrição                                            | Responsável |
|------------------------------------------------------|-------------|
| Testes unitários                                     | Pedro       |
| Automação na esteira. build + testes                 | Pedro       |
| Senha está criptografada no banco                    | Weslley     |
| Seed de banco de dados                               | Weslley     |
| Code first                                           | Weslley     |
| Não temos dados sensíveis no repo                    | Ewerson     |
| Autenticação JWT                                     | Ewerson     |
| Autorização. Usuário só vê e edita as coisas dele    | Ewerson     |
| Code review forte                                    | Mauricio    |
| Api rest seguindo os verbos e status corretamente    | Mauricio    |
| CORS. Facilitando a vida dos devs de front           | Mauricio    |
| Bom design, com separação de camadas                 | Jesus       |
| Code style consistente. ( inglês )                   | Jesus       |




## Ferramentas Recomendadas

### Desenvolvimento

- **Visual Studio Community**: Ideal para usuários do Windows.
  - [Download Visual Studio Community](https://visualstudio.microsoft.com/vs/community/)
- **Visual Studio Code**: Recomendado para usuários de Linux, com extensões como C#,.NET Core Tools e Swagger.
  - [Download Visual Studio Code](https://code.visualstudio.com/)


- **.NET SDK** (Versão 8.0.5 recomendada)
  - [Baixe o.NET SDK](https://dotnet.microsoft.com/download)

### Banco de Dados

- **SQL Server**
  - [Documentação oficial do SQL Server](https://docs.microsoft.com/en-us/sql/sql-server/)

### Outras Ferramentas

- **Azure Data Studio** para gerenciamento de SQL Server.
  - [Download Azure Data Studio](https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio)

## Modelo de Dados

![Modelo de Dados](https://github.com/raffacabofrio/acelera-dev-todo-list/blob/main/docs/MODELO.drawio.png)

## Como Executar

1. Clone o repositório:
bash git clone https://github.com/raffacabofrio/acelera-dev-todo-list
- cd acelera-dev-todo-list


3. Restaure as dependências necessárias:
- bash dotnet restore

4. Configure o banco de dados:

   Abra o Azure Data Studio.
   Conecte-se ao SQL Server.
   Execute os scripts SQL encontrados na pasta /src/Infra/ para configurar o banco de dados.

5. Execute a aplicação:
- dotnet run --project src/Api


6. Este comando irá iniciar o servidor e a API estará acessível em http://localhost:5042.

   Navegue para http://localhost:5042/swagger para ver e interagir com a documentação da API e testar os endpoints.

## Autores


- [Raffaello Damgaard](https://github.com/raffacabofrio)
- [Vinícius Silva](https://github.com/viniciusapsilva)
- [Paulo Ewerson](https://github.com/PauloEwerson)
- [Maurício Mafra](https://github.com/Mauricio-Mafra)
- [Jesus Wildes](https://github.com/GhortheBrute)
- [Weslley Batista](https://github.com/wesbats)
- [Pedro Augusto](https://github.com/eusouumx1)

## Relacionados

## Obter o appsettings com o time.
appsettings.Development.json


Segue o link do Trello relacionado

[Trello - Acelera Dev](https://trello.com/b/zJeRGV84/acelera-dev-todo-list)
"""