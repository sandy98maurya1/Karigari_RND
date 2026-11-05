# Karigari RND

Karigari RND is a .NET Core Web API for a labour and employer marketplace. The solution includes account and OTP workflows, JWT authentication, skill-set management, and SQL Server data access.

## Features

- Employee and employer account workflows
- JWT-based authentication and role authorization
- SMS and email OTP generation and verification
- Skill-set management
- SQL Server persistence with Entity Framework Core
- Swagger UI in development mode

## Technology

- ASP.NET Core 3.1
- Entity Framework Core 3.1
- SQL Server
- JWT Bearer authentication
- Swagger / Swashbuckle

## Repository structure

```text
Karigari_RND/
├── Contracts/          # Service and data interfaces
├── Data/               # EF Core context and data access
├── Karigari/           # ASP.NET Core API and controllers
├── Models/             # Domain models and enums
├── NotificationManager/
├── OTPGenerator/       # Email and SMS OTP helpers
├── Repository/         # Repository implementations
├── Utility/            # Shared utilities and constants
├── Karigari.sln       # Visual Studio solution
├── .gitignore
├── .gitattributes
├── README.md
└── .github/           # if present