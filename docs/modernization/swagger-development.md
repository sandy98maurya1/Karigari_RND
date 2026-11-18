# Swagger for local API development

The API uses Swashbuckle to generate an OpenAPI v1 document and provide Swagger UI.

## Local development

Run the API with the Development environment:

```cmd
set ASPNETCORE_ENVIRONMENT=Development
dotnet run --project src\Karigari.Worker.Api\Karigari.Worker.Api.csproj
```

Use the actual HTTP/HTTPS address printed by ASP.NET Core. For example, if the API listens on `http://localhost:54526`:

- Swagger UI: `http://localhost:54526/swagger`
- OpenAPI JSON: `http://localhost:54526/swagger/v1/swagger.json`
- Liveness: `http://localhost:54526/health/live`

Swagger middleware is enabled only when the ASP.NET Core environment is `Development`. It is not exposed by this configuration in Production.

## Notes

- The current API has only a liveness endpoint; Swagger will list that endpoint.
- Adding Swagger does not add worker registration endpoints, authentication, persistence, or database connectivity.
- The package version is pinned to Swashbuckle.AspNetCore 10.3.1.
