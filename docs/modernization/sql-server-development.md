# Isolated SQL Server development setup

## Safety boundary

- The historical database is currently unverified and its backup status is unknown.
- Do not connect this project to a legacy, shared, hosted or production SQL Server.
- Do not restore a backup until its provenance, contents and malware/security status are checked.
- Do not create or modify a database on an unknown server.
- The new local development database name is **KarigariDev**. It must be created only on a developer-controlled local SQL Server instance.

## Recommended local options

Use a locally installed SQL Server Developer Edition or SQL Server Express instance. SQL Server LocalDB is also acceptable for an individual Windows developer if it is already installed and suitable for the chosen tooling.

Record the exact SQL Server version and instance name in local notes; do not commit credentials or machine-specific values.

## Windows Command Prompt — verify local tooling

```cmd
dotnet --info
sqlcmd -?
```

If `sqlcmd` is installed and a local Windows-authenticated SQL Server instance is available, verify the connection first:

```cmd
sqlcmd -S .\SQLEXPRESS -E -Q "SELECT @@VERSION;"
```

Only after confirming that this is your own local development instance, create the isolated database:

```cmd
sqlcmd -S .\SQLEXPRESS -E -Q "IF DB_ID(N'KarigariDev') IS NULL CREATE DATABASE [KarigariDev];"
```

Replace `.\\SQLEXPRESS` only with the instance name you have verified. Do not substitute a production or unknown server.

## Application configuration

The API scaffold intentionally has no connection string and no EF Core provider yet. After the local database instance is confirmed and the persistence design is approved:

1. Add a compatible SQL Server provider and EF Core tooling version.
2. Initialize .NET User Secrets for the API project.
3. Store the local connection string in User Secrets, not appsettings or source control.
4. Use synthetic test data only.
5. Keep integration tests pointed at a separate disposable test database.

Example command shape (replace the local server/instance only after verification):

```cmd
dotnet user-secrets init --project src\Karigari.Worker.Api\Karigari.Worker.Api.csproj
dotnet user-secrets set "ConnectionStrings:Karigari" "Server=.\SQLEXPRESS;Database=KarigariDev;Integrated Security=True;TrustServerCertificate=True;" --project src\Karigari.Worker.Api\Karigari.Worker.Api.csproj
```

Do not put a SQL username/password in this example. Prefer Windows authentication locally where available. For Azure, use managed identity and a separately provisioned database identity.

## Before any schema work

- Confirm local SQL Server version and availability.
- Confirm whether legacy data is required.
- Obtain and validate a known-good backup only if the owner can locate one.
- Compare the historical 2022 SQL script with any recovered live schema.
- Design new migrations against KarigariDev only.
