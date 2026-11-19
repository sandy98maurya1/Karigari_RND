# Isolated SQL Server development setup

## Safety boundary

- Karigari 2.0 uses the isolated local development database **KarigariWorkerDev**.
- Do not connect this project to the legacy `KarigariUserManagement` database or any hosted/production SQL Server.
- Use synthetic test data only.

## Verified local target

The confirmed local instance is `localhost\\SQLEXPRESS`, using Windows authentication. The isolated database `KarigariWorkerDev` was created for this project and verified ONLINE with no user tables at the time of setup.

## Windows Command Prompt — configure User Secrets

Run from the repository root:

```cmd
dotnet user-secrets init --project src\Karigari.Worker.Api\Karigari.Worker.Api.csproj
dotnet user-secrets set "ConnectionStrings:Karigari" "Server=localhost\SQLEXPRESS;Database=KarigariWorkerDev;Integrated Security=True;TrustServerCertificate=True;" --project src\Karigari.Worker.Api\Karigari.Worker.Api.csproj
```

Keep the connection string in User Secrets; never commit it. For Azure, use managed identity and a separately provisioned database identity.

## EF Core migration workflow

Install the EF Core CLI if needed:

```cmd
dotnet tool install --global dotnet-ef --version 10.0.15
```

Create migration source files:

```cmd
dotnet ef migrations add InitialWorkerPersistence --project src\Karigari.Worker.Infrastructure\Karigari.Worker.Infrastructure.csproj --startup-project src\Karigari.Worker.Api\Karigari.Worker.Api.csproj --output-dir Persistence\Migrations
```

Review the migration and generated SQL before applying it. Applying a migration changes the development database; do not run `database update` until the schema has been reviewed and approved.

Never target `KarigariUserManagement` or production.
