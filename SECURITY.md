# Security remediation — Karigari

## Immediate action required

A tracked application configuration file in the historical Karigari source contains values that appear to be database credentials and JWT signing material. Treat these values as compromised. This notice intentionally does not reproduce them.

### Required actions (in order)
1. Rotate the SQL Server/database password at the database provider. Update only the authorized secret store after rotation.
2. Replace the JWT signing key with a cryptographically random secret of appropriate length. Invalidate existing tokens/sessions if the application uses the exposed key.
3. Check and rotate related email, SMS, hosting, storage, or third-party credentials found in other Karigari repositories and Git history.
4. Remove secret values from tracked configuration. Commit only placeholders and safe local-development examples.
5. Configure local secrets using .NET User Secrets or environment variables. Use Azure Key Vault and managed identity for Azure-hosted environments.
6. Scan every repository and all Git history for credentials. After rotating them, purge exposed values from history where appropriate. History rewriting changes commit IDs and requires collaborators to re-clone or reset.
7. Review repository visibility, forks, clones, CI logs, deployment logs, and provider access logs where available.
8. Add automated secret scanning and prevent future secret commits.

## Safe configuration pattern
- appsettings.json: non-secret defaults only.
- appsettings.Development.json: non-secret developer defaults only.
- User Secrets: local developer credentials.
- Environment variables: deployment configuration.
- Azure Key Vault: production secrets, accessed using managed identity.

Never commit actual passwords, API keys, connection strings, signing keys, private certificates, or personal data.

## Completion checklist
- [ ] Database password rotated and old credential revoked.
- [ ] JWT key rotated and old tokens invalidated.
- [ ] Other provider credentials reviewed and rotated as needed.
- [ ] Tracked configuration contains placeholders only.
- [ ] Git history reviewed and sanitized after rotation.
- [ ] Secret scanning enabled.
- [ ] Access logs reviewed where available.
- [ ] Non-production deployment verified with new secrets.

This document is a remediation checklist, not evidence that remediation has been completed.