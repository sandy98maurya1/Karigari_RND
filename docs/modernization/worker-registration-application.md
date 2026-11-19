# Worker registration — application and persistence increment

## Implemented

- `RegisterWorkerCommand`, `RegisterWorkerHandler`, and `RegisterWorkerResult`.
- `IWorkerProfileRepository` persistence boundary.
- EF Core SQL Server `WorkerDbContext`, entity mapping, and repository adapter.
- Dependency injection for the application handler and SQL Server adapter.
- HTTP endpoint: `POST /api/v1/workers/registrations`.

## Registration behavior

- Successful registration persists a profile and returns HTTP 201 with the generated worker ID.
- Invalid domain input returns HTTP 400.
- New profiles remain inactive until a separately designed contact-verification workflow activates them.
- Contact-number format validation does not prove ownership or replace OTP verification.
- Credentials, OTPs, and token material are excluded from the worker profile.

## Database boundary

The adapter uses isolated `KarigariWorkerDev` through `ConnectionStrings:Karigari`. Store the local connection string in .NET User Secrets. Do not connect to or modify `KarigariUserManagement` or production.

The EF migration must be generated and reviewed before applying it. Application startup does not automatically modify the database.
