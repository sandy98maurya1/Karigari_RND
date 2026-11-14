# Worker registration and profile — first-slice discovery contract

This is a proposed discovery/API contract, not an implemented or published API. Final route names, fields and validation rules must be reconciled with the legacy controllers, database schema and product requirements before coding.

## Candidate resources

- Worker account: authentication identity and account status.
- Worker profile: worker-owned professional/personal profile data.
- Skill: normalized skill catalogue entry.
- Worker skill: relationship between a worker and a skill, optionally including experience/proficiency if the product requires it.
- Verification challenge: one-time contact-verification challenge with expiry, attempt count and consumption state.

## Candidate endpoints

| Method | Candidate route | Purpose |
|---|---|---|
| POST | /api/v1/workers/registrations | Create worker account and initiate verification |
| POST | /api/v1/workers/registrations/verify | Verify contact challenge |
| POST | /api/v1/auth/token | Authenticate verified worker |
| GET | /api/v1/workers/me/profile | Read current worker profile |
| PUT | /api/v1/workers/me/profile | Create or replace current worker profile |
| PATCH | /api/v1/workers/me/profile | Partially update current worker profile, if needed |
| GET | /api/v1/skills | Browse available skills |

## Security and behaviour requirements

- Derive worker identity from the authenticated principal; never accept a caller-supplied worker ID as authority for a self-service profile operation.
- Require authentication and ownership authorization for profile read/update operations.
- Normalize and validate email/phone according to confirmed product rules.
- Apply duplicate-account rules consistently and avoid account-enumeration leaks.
- OTPs must be random, short-lived, single-use, attempt-limited and rate-limited; store a secure verifier/hash rather than plaintext where practical.
- Never log passwords, OTPs, tokens, signing keys or connection strings.
- Use HTTPS; production authentication must require HTTPS metadata and strict issuer/audience/signature/lifetime validation.
- Return consistent problem details and avoid exposing stack traces in production.
- Use optimistic concurrency or another documented strategy if concurrent profile edits are possible.

## Required discovery before implementation

1. Read every worker registration/profile controller, DTO, model, repository and data-layer implementation in the legacy repository.
2. Inspect EF Core DbContext, entity configurations, migrations and actual SQL schema.
3. Inspect the other Karigari repositories for identity/token/OTP dependencies and API contracts.
4. Confirm which fields are mandatory and which are visible publicly versus private to the worker.
5. Confirm existing data compatibility and whether existing workers must be migrated without forcing password resets.
6. Build and test the legacy solution to record the baseline before porting.

Do not infer business rules from class names alone. Treat existing source behaviour and the owner's confirmed product decisions as the source of truth.