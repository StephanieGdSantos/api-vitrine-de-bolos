# API CardapioBolos

## Visão Geral

A API CardapioBolos é projetada para gerenciar um catálogo de bolos, incluindo seus ingredientes, pedidos e administradores. Esta API permite que os usuários realizem operações CRUD em bolos, gerenciem pedidos e lidem com tarefas administrativas. É importante pontuar que, devido ao contexto para o qual foi criada, ela não possui integração para pagamentos.

## Índice

- [Introdução](#introdução)
- [Endpoints](#endpoints)
- [Modelos](#modelos)
- [Banco de Dados](#banco-de-dados)
- [Executando o Projeto](#executando-o-projeto)
- [Licença](#licença)

## Introdução

### Pré-requisitos

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- [Docker](https://www.docker.com/products/docker-desktop) (opcional, para implantação em contêiner)

### Instalação

1. Clone o repositório:
    ```sh
    git clone https://github.com/StephanieGdSantos/api-vitrine-de-bolos.git
    cd CardapioBolos
    ```

2. Restaure as dependências:
    ```sh
    dotnet restore
    ```

3. Atualize o banco de dados:
    ```sh
    dotnet ef database update
    ```

## Endpoints

### Bolos

- **GET /api/bolos**: Recupera uma lista de todos os bolos.
- **GET /api/bolos/{id}**: Recupera detalhes de um bolo específico pelo ID.
- **POST /api/bolos**: Cria um novo bolo.
- **PUT /api/bolos/{id}**: Atualiza um bolo existente pelo ID.
- **DELETE /api/bolos/{id}**: Exclui um bolo pelo ID.

### Encomendas

- **GET /api/encomendas**: Recupera uma lista de todas as encomendas.
- **GET /api/encomendas/{id}**: Recupera detalhes de uma encomenda específica pelo ID.
- **POST /api/encomendas**: Cria uma nova encomenda.
- **PUT /api/encomendas/{id}**: Atualiza uma encomenda existente pelo ID.
- **DELETE /api/encomendas/{id}**: Exclui uma encomenda pelo ID.

### Administradores

- **GET /api/administradores**: Recupera uma lista de todos os administradores.
- **GET /api/administradores/{id}**: Recupera detalhes de um administrador específico pelo ID.
- **POST /api/administradores**: Cria um novo administrador.
- **PUT /api/administradores/{id}**: Atualiza um administrador existente pelo ID.
- **DELETE /api/administradores/{id}**: Exclui um administrador pelo ID.

## Modelos

### Bolo

- `id`: string
- `preco`: decimal
- `nome`: string
- `descricao`: string
- `listaDeIngredientes`: List<string>
- `peso`: int
- `imagem`: string

### Encomenda

- `id`: string
- `cliente`: string
- `data`: DateTime
- `bolos`: List<BoloEncomenda>

### Administrador

- `id`: string
- `nome`: string
- `email`: string
- `senha`: string

### BoloIngrediente

- `boloId`: string
- `ingredienteId`: string

### BoloEncomenda

- `boloId`: string
- `encomendaId`: string

## Banco de Dados

O contexto do banco de dados é definido em CardapioBolosContext.cs e inclui as seguintes propriedades DbSet:

- DbSet<Bolo> Bolos
- DbSet<Encomenda> Encomendas
- DbSet<Administrador> Administrador
- DbSet<BoloIngrediente> BoloIngrediente
- DbSet<BoloEncomenda> BoloEncomenda

## Executando o Projeto

### Usando .NET CLI

1. Compile o projeto:
    ```sh
    dotnet build
    ```

2. Execute o projeto:
    ```sh
    dotnet run
    ```

### Usando Docker

1. Baixe a imagem Docker:
    ```sh
    docker pull stephaniegomes/vitrine-bolos:latest
    ```

2. Execute o contêiner Docker:
    ```sh
    docker run -p 5000:8080 stephaniegomes/vitrine-bolos
    ```
