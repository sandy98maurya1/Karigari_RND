# Worker API scaffold — Phase 1

## What is added

- `Karigari.Worker.slnx`
- `Karigari.Worker.Api`: ASP.NET Core host with a liveness endpoint.
- `Karigari.Worker.Application`: application-layer boundary.
- `Karigari.Worker.Domain`: domain-layer boundary.
- `Karigari.Worker.Infrastructure`: infrastructure registration boundary.
- Shared .NET 10 build settings.
- Initial test strategy and isolated SQL Server setup instructions.

## Deliberate omissions

This is a compile-first scaffold, not a working registration service. It does not contain:
- User registration, profile endpoints, entities or DTOs.
- Authentication, JWT issuance, password handling or OTP integration.
- EF Core, database migrations or a SQL connection.
- Any legacy credentials, production settings or real user data.
- Azure deployment configuration.

These omissions are intentional. Worker fields and validation rules must be mapped from legacy code and confirmed product requirements. The existing database and backup status remain unknown. Authentication must be designed independently of the retired token service.

## Dependency direction

```text
API -> Application -> Domain
API -> Infrastructure -> Application + Domain
```

The domain layer has no infrastructure dependency. The infrastructure layer currently contains no database provider until a local isolated SQL Server is verified.

## Local build commands (Windows Command Prompt)

Run from the repository root after installing the .NET 10 SDK:

```cmd
dotnet --info
dotnet restore Karigari.Worker.slnx
dotnet build Karigari.Worker.slnx --no-restore
dotnet run --project src\Karigari.Worker.Api\Karigari.Worker.Api.csproj
```

Then browse to the local address printed by ASP.NET Core and request `/health/live`.

The build has not been run in this remote repository operation. Record the actual SDK version and build output before claiming the scaffold compiles.
