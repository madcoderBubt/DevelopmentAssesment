# ProjectManagement System

A comprehensive .NET 8 Web API solution designed for managing projects, teams, and tasks. This application demonstrates modern software architecture practices including Clean Architecture, CQRS, and .NET Aspire orchestration.

## 🏗 Architecture

The solution follows **Clean Architecture** principles, separating concerns into distinct projects:

- **ProjectManagement.AppHost**: [.NET Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/) orchestration project for managing application resources.
- **ProjectManagement (API)**: The entry point of the application, an ASP.NET Core Web API.
- **ProjectManagement.Core**: Contains the domain entities (User, Team, Task) and business logic.
- **ProjectManagement.Infrastructure**: Handles data access, database migrations, and external service implementations.
- **ProjectManagement.UnitTest**: Unit testing project.

## 🚀 Key Features

- **CQRS Pattern**: Implements Command Query Responsibility Segregation for separation of read and write operations.
- **Authentication & Authorization**: Secure JWT Bearer authentication.
- **Data Access**: Entity Framework Core with SQL Server using the Repository Pattern.
- **Validation**: Request validation using FluentValidation.
- **Global Error Handling**: Custom middleware for consistent error responses (`GlobalExeptionHandleMiddleware`).
- **API Documentation**: Automated OpenAPI/Swagger documentation.
- **Orchestration**: Built with .NET Aspire for easy cloud-native development.

## 🛠 Tech Stack

- **Framework**: .NET 8, ASP.NET Core
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Validation**: FluentValidation
- **Testing**: xUnit (implied)
- **Containerization/Orchestration**: .NET Aspire

## ⚙️ Configuration

The application requires the following configuration in `appsettings.json`:

### Connection String
Ensure you have a SQL Server instance running and update the `DefaultConnection`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=...;Database=ProjectManagementDb;..."
}
```

### JWT Settings
Configure your JWT secret and issuer settings:
```json
"Jwt": {
  "Key": "YourSecretKeyHere",
  "Issuer": "YourIssuer",
  "Audience": "YourAudience"
}
```

## ▶️ How to Run

### Using Visual Studio
1.  Open `ProjectManagement.sln`.
2.  Set `ProjectManagement.AppHost` as the startup project.
3.  Press **F5** to run. The Aspire Dashboard will launch, allowing you to access the API and other resources.

### Using CLI
Navigate to the AppHost directory and run:
```bash
cd ProjectManagement/ProjectManagement.AppHost
dotnet run
```

## 📄 API Endpoints

The API includes controllers for:
- **Auth**: User registration and login.
- **User**: User management operations.
- **Projects/Tasks**: (Implied by domain entities) Management of project resources.

Access the Swagger UI at `/swagger` when running the API project locally to explore all endpoints.