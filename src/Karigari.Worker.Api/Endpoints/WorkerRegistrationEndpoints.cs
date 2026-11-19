using Karigari.Worker.Api.Contracts.Workers;
using Karigari.Worker.Application.Workers.Registration;

namespace Karigari.Worker.Api.Endpoints;

public static class WorkerRegistrationEndpoints
{
    public static IEndpointRouteBuilder MapWorkerRegistrationEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/v1/workers/registrations", RegisterAsync)
            .WithName("RegisterWorker")
            .WithTags("Workers")
            .WithSummary("Register a worker profile")
            .WithDescription("Creates an inactive worker profile. Contact ownership verification is a separate workflow.")
            .Produces<RegisterWorkerResult>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        return endpoints;
    }

    private static async Task<IResult> RegisterAsync(
        RegisterWorkerRequest request,
        RegisterWorkerHandler handler,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new RegisterWorkerCommand(
                request.FirstName, request.LastName, request.ContactNumber,
                request.AlternateContactNumber, request.DateOfBirth);
            var result = await handler.HandleAsync(command, cancellationToken).ConfigureAwait(false);
            return Results.Created($"/api/v1/workers/{result.WorkerId}", result);
        }
        catch (ArgumentException exception)
        {
            return Results.Problem(
                title: "Invalid worker registration",
                detail: exception.Message,
                statusCode: StatusCodes.Status400BadRequest);
        }
    }
}
