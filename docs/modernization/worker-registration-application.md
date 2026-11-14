# Worker registration — application increment

## Implemented

- `RegisterWorkerCommand` defines the application input.
- `RegisterWorkerHandler` creates a domain `WorkerProfile` and delegates persistence.
- `IWorkerProfileRepository` defines the persistence boundary.
- `RegisterWorkerResult` returns the persisted worker ID and safe profile fields.

## Deliberate boundaries

- No HTTP endpoint is exposed in this increment. The API endpoint will be added together with a real persistence adapter so the API does not claim registration succeeded when nothing was stored.
- No in-memory or fake repository is registered.
- No SQL Server provider, connection string, database creation, migration execution, or database write is included.
- The repository implementation must target a new, isolated development database. The existence and backup status of any historical Karigari database remain unverified.
- New profiles remain inactive until a separately designed verification workflow activates them.
- Contact-number validation does not verify ownership or replace OTP verification.

## Next increment

Implement the SQL Server persistence adapter and schema for a new isolated development database, then wire the application handler into `POST /api/v1/workers`. Before database setup, confirm the local SQL Server instance and the intended isolated development database name.
