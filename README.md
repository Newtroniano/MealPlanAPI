
# MealPlanAPI

API para gerenciamento de planos alimentares, desenvolvida em .NET 8, com autenticação JWT e documentação via Swagger. Permite que nutricionistas gerenciem pacientes, alimentos e planos alimentares de forma eficiente.

## Funcionalidades

- Autenticação via JWT
- CRUD de pacientes
- CRUD de alimentos com busca por valores nutricionais
- CRUD de planos alimentares
- Documentação interativa via Swagger

## Pré-requisitos

- .NET SDK 8.0 ou superior
- SQL Server (local ou remoto)

## Instalação

### 1. Clonar o repositório

```bash
git clone https://github.com/seuusuario/MealPlanAPI.git
cd MealPlanAPI
```

### 2. Instalar dependências

```bash
dotnet add package AutoMapper --version 14.0.0
dotnet add package DotNetEnv --version 3.1.1
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.16
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 8.0.16
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.16
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.16
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.16
dotnet add package Swashbuckle.AspNetCore --version 6.6.2
```

### 3. Configurar o banco de dados

Edite o arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MealPlanDB;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "sua_chave_secreta_super_segura_aqui",
    "Issuer": "MealPlanAPI",
    "Audience": "MealPlanAPIUsers",
    "DurationInMinutes": 60
  }
}
```

### 4. Aplicar migrações

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5. Executar a aplicação

```bash
dotnet run
```

A API estará disponível em:

```
http://localhost:5000
```

## Documentação Swagger

Acesse:

```
http://localhost:5000/swagger
```

## Autenticação

Para obter um token JWT:

**Requisição:**

```
POST /api/Auth/login
```

**Corpo:**

```json
{
  "email": "nutritionist",
  "password": "Nutritionist123!"
}
```

**Credenciais padrão:**

| Função        | Usuário       | Senha             |
|----------------|----------------|-------------------|
| Nutricionista  | nutritionist   | Nutritionist123!  |
| Admin          | admin          | Admin123!         |

## Endpoints Principais

### Pacientes

| Método | Endpoint                         | Descrição                            |
|--------|-----------------------------------|---------------------------------------|
| POST   | /api/Patient                      | Cria um paciente                     |
| GET    | /api/Patient                      | Lista pacientes                      |
| GET    | /api/Patient/{id}                 | Detalhes de um paciente              |
| PUT    | /api/Patient/{id}                 | Atualiza um paciente                 |
| DELETE | /api/Patient/{id}                 | Remove um paciente                   |
| PATCH  | /api/Patient/{id}/reactivate      | Reativa um paciente removido         |
| GET    | /api/Patient/{id}/mealplans/today | Plano alimentar do dia do paciente   |

### Alimentos

| Método | Endpoint                     | Descrição                            |
|--------|-------------------------------|---------------------------------------|
| POST   | /api/Foods                    | Cria um alimento                     |
| GET    | /api/Foods                    | Lista alimentos                      |
| GET    | /api/Foods/{id}               | Detalhes de um alimento              |
| PUT    | /api/Foods/{id}               | Atualiza um alimento                 |
| DELETE | /api/Foods/{id}               | Remove um alimento                   |
| PATCH  | /api/Foods/{id}/reactivate    | Reativa um alimento removido         |
| GET    | /api/Foods/nutrition          | Busca alimentos por valor nutricional|

### Planos Alimentares

| Método | Endpoint               | Descrição                            |
|--------|-------------------------|---------------------------------------|
| POST   | /api/mealplans          | Cria um plano alimentar              |
| GET    | /api/mealplans/{id}     | Detalhes de um plano alimentar       |
| PUT    | /api/mealplans/{id}     | Atualiza um plano alimentar          |
| DELETE | /api/mealplans/{id}     | Remove um plano alimentar            |

## Estrutura do Projeto

```
MealPlanAPI/
├── Controllers/       # Controladores da API
├── Models/            # Entidades e DTOs
├── Services/          # Regras de negócio
├── Data/              # Configurações do banco e contexto
├── Migrations/        # Migrações do Entity Framework
└── appsettings.json   # Configurações da aplicação
```

## Contribuição

