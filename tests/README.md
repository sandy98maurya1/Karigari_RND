# Worker API test plan

No test project or test framework is selected yet. Confirm the package versions against the .NET 10 target before adding dependencies.

## Test layers

1. **Domain unit tests**
   - Required-field and value-object rules after worker fields are confirmed.
   - Normalization rules for phone/email only after product rules are agreed.
2. **Application tests**
   - Registration and profile use cases with repository/verification abstractions.
   - Duplicate registration and validation outcomes.
3. **API tests**
   - Request validation and consistent Problem Details responses.
   - Authentication and authorization once the identity design is selected.
   - A worker cannot read or update another worker's profile.
4. **Infrastructure integration tests**
   - SQL Server persistence against an isolated disposable test database.
   - Migration up/down or repeatability checks.
   - No test may point at an unknown or production database.
5. **Security regression tests**
   - OTP expiry, single use, attempt limits and rate limits if OTP is brought back.
   - No password, OTP, token, signing key or connection string in logs or responses.

## Initial gate

The scaffold must compile before business code is added. The first vertical slice is not considered complete until registration/profile tests and SQL Server integration tests pass.
