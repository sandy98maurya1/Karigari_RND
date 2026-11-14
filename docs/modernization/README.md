# Karigari 2.0 — Modernization Plan

## Product intent

Karigari is the owner's personal product initiative: a digital employment marketplace connecting workers/job seekers and employers. The restart has two goals: resume product development with a secure, maintainable foundation, and build a truthful engineering portfolio from the real product.

The current repository README describes employee and employer account workflows, JWT authentication, email/SMS OTP, skill-set management, SQL Server persistence, and Swagger. These are baseline capabilities to validate against the actual source; this document does not assume they are complete or production-ready.

## Working principles
- Preserve the original product intent and existing working behaviour.
- Do not rewrite all services at once.
- Make changes on feature branches and use pull requests.
- Establish build/test baselines before changing frameworks.
- Never commit secrets, customer data, personal data, or production connection strings.
- Keep architecture decisions documented and reversible where possible.
- Do not claim features, performance gains, security posture, or deployment status until verified.

## Initial technical direction

| Area | Initial direction | Status |
|---|---|---|
| Backend | .NET 10 / ASP.NET Core 10 LTS | Proposed baseline |
| Database | Retain existing SQL Server for first increment | Provisional; reassess after schema/hosting review |
| Data access | EF Core compatible with .NET 10 | Proposed |
| Authentication | Review current ASP.NET Identity/JWT; design secure token and OTP lifecycle | Assessment required |
| API documentation | OpenAPI/Swagger with environment-aware exposure | Proposed |
| Hosting | Azure App Service candidate | Confirm deployment and cost requirements |
| Secrets | .NET User Secrets locally; Azure Key Vault in Azure | Required |
| Frontend | Preserve and audit existing UI before choosing upgrade/rewrite | Assessment required |
| CI | GitHub Actions build, test, dependency and secret checks | Proposed |

.NET 10 is selected over immediate .NET 11 adoption because this restart prioritizes an LTS support window and predictable production servicing. Revisit at a planned upgrade checkpoint.

## Phase 0 — Safety and discovery (blocking)
1. Rotate every credential found in tracked configuration or history, including database passwords and JWT signing keys.
2. Revoke/replace provider credentials discovered in all Karigari repositories.
3. Remove secrets from current tracked files and configure secret injection.
4. Review Git history and repository visibility. After rotation, purge exposed secrets from history where appropriate and coordinate force-push/re-clone steps.
5. Inventory repositories, branches, project files, frameworks, NuGet dependencies, UI framework, database schema, external services, and deployment state.
6. Build the existing solution and record exact failures before refactoring.
7. Create a feature inventory and identify which workflows still work.

Do not deploy or expose the old application until credential rotation and security review are complete.

## Phase 1 — First service modernization
Use Karigari_RND as the first vertical slice:
1. Create a clean .NET 10 solution/project structure in a separate modernization branch.
2. Port one thin end-to-end workflow while preserving its API contract where practical.
3. Separate domain/application/infrastructure/API responsibilities where this improves testability; avoid ceremony.
4. Add request validation, centralized error handling, structured logging, health checks, and OpenAPI.
5. Move configuration and secrets out of source control.
6. Add unit and integration tests; validate authorization and OTP expiry/rate limiting.
7. Run build, tests, dependency checks, and security scans in CI.

Do not mechanically retarget every project before checking package compatibility and runtime behaviour.

## Phase 2 — Product and service boundaries
Review TokenServer, UserService, UI, and Gateway together. Decide whether each remains a separate deployable service or is consolidated into a modular monolith for the MVP. Service boundaries must follow business ownership and operational needs, not repository count.

## Phase 3 — Delivery and operations
- Containerize only when it improves local parity or deployment.
- Add GitHub Actions with build/test/security gates.
- Deploy a non-production Azure environment after secrets and identity are ready.
- Add monitoring, logs, alerts, backups, and deployment rollback.
- Document cost assumptions and environment separation.

## Phase 4 — Portfolio readiness
- Product overview and architecture diagrams.
- Local setup and configuration guide with safe example settings.
- API documentation and sample requests using synthetic data.
- Test strategy and CI status.
- Screenshots/demo using synthetic data only.
- Architecture decision records and a truthful changelog.

## Definition of done for the first increment
- No secrets in tracked files or current branch.
- .NET 10 solution builds reproducibly.
- Core selected workflow passes automated tests.
- Authentication/authorization and OTP controls are tested.
- Configuration is environment-driven.
- CI runs build and tests.
- README explains setup, architecture, and limitations.
- No claims of production readiness without an operational review.

## Immediate open questions
- Is the original SQL Server database still accessible, and is its data needed?
- Which frontend framework/version is used by Karigari_UI?
- Which OTP/email/SMS providers are still active?
- Which user journey is the first MVP journey: worker registration/profile, employer registration, job posting, or job search?
- Is any old deployment still running, and who controls its hosting account?