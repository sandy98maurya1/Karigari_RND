# Worker domain model — first increment

## Implemented

The first domain increment adds:

- `WorkerProfile`, with the historical worker identifier represented as a 64-bit integer.
- Required first name, last name and primary contact number.
- Optional alternate contact number and date of birth.
- An inactive-by-default profile lifecycle with explicit activate/deactivate operations.
- `ContactNumber`, which removes common visual separators and validates a generic 7–15 digit range.

## Deliberate boundaries

- Passwords, refresh tokens, OTPs, signing keys and other authentication material do not belong to the worker profile.
- Email is not included because the inspected legacy `Labour` model does not expose an email field. Add it only after the product and account model requirements are confirmed.
- Address, gender, marital status and worker type are deferred until their legacy representations, enumerations and product requirements are reconciled.
- Contact-number validation is intentionally country-neutral. It does not prove ownership or replace OTP verification.
- The profile starts inactive. A future, separately designed verification/account workflow must decide when activation is allowed.

## Compatibility notes

The historical `Labour` model uses a `long` identifier and includes first name, last name, contact, alternate contact, date of birth and active state. This domain model preserves those confirmed concepts while excluding credential fields.

The historical SQL schema is not evidence of a live database. No SQL Server provider, connection string, migration, database operation or deployment is introduced by this change.

## Validation to run locally

From the repository root in Windows Command Prompt:

```cmd
dotnet build Karigari.Worker.slnx
```

The domain project has no new external package dependencies.
