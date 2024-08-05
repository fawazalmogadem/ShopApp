using BuildingBlocks.CQRS;


namespace Catalog.API.Products.CreateProduct;
public record CreateProductRequest(Guid Id, string Name, string Description,
    List<string> Category, string ImageFile, decimal Price);
public record CreateProductResponse(Guid id);

public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResult>();
            return Results.Created($"/products/{response.id}", response);
        })
            .WithName("CreateProduct")
            .WithDescription("Create Product")
            .WithSummary("Create Product")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
