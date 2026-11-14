# ADR-001: Karigari 2.0 initial modernization decisions

- Status: Accepted for the first modernization increment
- Date: 2026-11-14

## Context

Karigari is a personal product initiative being restarted as both a real employment marketplace and the owner's engineering portfolio. Development had stopped. The first user journey is worker registration and profile management. The existing API is ASP.NET Core 3.1 with Entity Framework Core 3.1 and SQL Server.

## Decisions

1. **First vertical slice:** worker registration and profile management.
2. **Database:** retain SQL Server for the first migration. Do not migrate to PostgreSQL until the existing schema, data, hosting and operational constraints have been assessed.
3. **Hosting target:** Azure App Service. Deployment is not authorized until credentials are rotated, configuration is externalized, and security checks pass.
4. **Backend target:** .NET 10 / ASP.NET Core 10 LTS for the new implementation. Verify package compatibility and supported versions before selecting dependencies.
5. **Migration approach:** incremental strangler-style modernization. Preserve the legacy branch and API behaviour where appropriate; do not perform a blind framework retarget or rewrite every service at once.
6. **Secrets:** no secrets in source control. Local development uses .NET User Secrets/environment variables; Azure uses managed identity and Key Vault.
7. **Testing:** add unit and integration tests for registration, profile update, validation, authorization and persistence before production deployment.

## First-slice functional scope

### In scope
- Register a worker account with validated, normalized identity/contact fields.
- Verify contact ownership using the existing OTP provider only after that provider and its credentials are reviewed.
- Authenticate the worker using the chosen secure identity/token design.
- Create and retrieve the authenticated worker's profile.
- Update profile fields with server-side validation and authorization.
- Manage skills as structured data, subject to existing schema and product rules.
- Prevent one worker from reading or modifying another worker's private profile data.
- Provide health/readiness endpoints and OpenAPI documentation in non-production environments.

### Out of scope for this first increment
- Employer registration and employer dashboards.
- Job posting, matching, applications and payments.
- Public production deployment.
- Introducing microservices, a message broker, Redis or a new database unless discovery demonstrates a concrete need.
- AI features in the product.

## Acceptance criteria

- Registration rejects invalid or duplicate identifiers according to confirmed product rules.
- Contact verification codes expire, are single-use, are rate-limited, and are never logged.
- Passwords are stored only using a modern, vetted password-hashing mechanism; never reversible or plaintext.
- Authentication tokens have explicit issuer, audience, expiry and signing-key configuration; signing material is stored outside source control.
- Profile read/update endpoints enforce ownership and least privilege.
- Validation and error responses are consistent and do not disclose secrets or internal exception details.
- Unit and integration tests cover happy paths, validation failures, duplicate registration, unauthorized access and cross-user access attempts.
- SQL Server migrations/schema changes are reviewed and repeatable.
- CI builds and runs tests using non-production configuration.
- No secrets are present in current tracked files or newly added commits.

## Consequences and follow-up

- SQL Server remains the initial persistence choice, reducing migration risk while legacy schema is understood.
- Azure App Service is the intended hosting destination, but hosting settings, region, identity, network access, cost and deployment slot strategy remain to be designed.
- Existing OTP, JWT and account code must be reviewed; do not assume it meets the acceptance criteria.
- Before implementation, rotate the credentials discovered in historical tracked configuration and inspect every related repository and Git history.

## Open questions

- Confirm required worker fields and whether phone, email, or both are mandatory.
- Confirm worker identity/verification rules and applicable privacy/retention requirements.
- Confirm the current SQL Server version, access, schema, backup and data migration needs.
- Confirm whether legacy OTP/email/SMS providers remain active and authorized.
- Confirm whether any legacy deployment is still running.