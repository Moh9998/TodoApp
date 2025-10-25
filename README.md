
# TodoApp – Clean Architecture ASP.NET Core 9

Implements Clean Architecture with CQRS (MediatR), EF Core (Postgres), ASP.NET Identity + JWT + refresh tokens,
FluentValidation, ProblemDetails, API versioning, Swagger with JWT, HTTP Client Factory, CORS, Docker (compose).

## Quick start (Docker)
```bash
docker compose up --build
# API http://localhost:8080 ; Swagger at /swagger
```

## Local dev
```bash
dotnet tool install --global dotnet-ef
dotnet restore
setx DB_CONN "Host=localhost;Port=5432;Database=todo_db;Username=postgres;Password=postgres"
# PowerShell: $env:DB_CONN="..."
dotnet ef migrations add Init -p src/TodoApp.Infrastructure -s src/TodoApp.WebApi
dotnet ef database update      -p src/TodoApp.Infrastructure -s src/TodoApp.WebApi
dotnet run --project src/TodoApp.WebApi
```
