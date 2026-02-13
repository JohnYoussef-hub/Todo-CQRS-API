# Todo CRUD API with CQRS

A simple Todo API built with **Clean Architecture** and **CQRS pattern** using .NET 10.

## Technologies

- .NET 10 - Core Framework
- Entity Framework Core - ORM
- MediatR - CQRS Pattern implementation
- FluentValidation - Input validation
- Serilog - Structured logging
- Swagger - API Documentation

## Architecture

The project is organized into 4 layers:

- **Domain** - Business entities (Todo)
- **Application** - Commands, Queries, and Handlers
- **Infrastructure** - Database and EF Core
- **API** - Controllers, Swagger configuration, and Serilog setup

## Getting Started

1. **Clone the repo**
   ```bash
   git clone https://github.com/yourusername/todo-cqrs-api.git
   ```

2. **Update connection string in `API/appsettings.json`**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=TodoCQRS;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

3. **Run migrations**
   ```bash
   cd API
   dotnet ef database update
   ```

4. **Run the app**
   ```bash
   dotnet run
   ```

- API runs at `https://localhost:7230`
- Swagger UI: `https://localhost:7230/swagger` (Interactive documentation)

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/todos` | Get all todos |
| GET | `/api/todos/{id}` | Get todo by ID |
| POST | `/api/todos` | Create new todo |
| PUT | `/api/todos/{id}` | Update todo |
| DELETE | `/api/todos/{id}` | Delete todo |

## Features

- CQRS Pattern - Complete separation of Commands and Queries.
- Clean Architecture - Decoupled layers for better maintainability.
- Structured Logging - Powered by Serilog.
- Automatic API Docs - Swagger UI for testing endpoints.
- Input Validation - FluentValidation integration with MediatR.
- Global Exception Handling - Automatic error logging and clean API responses.
