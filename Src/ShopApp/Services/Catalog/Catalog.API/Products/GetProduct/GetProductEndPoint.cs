
namespace Catalog.API.Products.GetProduct
{
    public class GetProductEndPoint : ICarterModule
    {
        public record GetProductResponse(Product Products);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products/{id}", async (Guid id,ISender sender) =>
            {
                var result = await sender.Send(new GetProductQuery(id));
                var response = result.Adapt<GetProductResponse>();
                return Results.Ok(response);
            })
                .WithName("GetProduct")
                .WithDescription("Get Product")
                .WithSummary("Get Product")
                .Produces<GetProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
