

namespace Catalog.API.Products.GetProductsByCategory
{
    public class GetProductsByCategoryEndPoint : ICarterModule
    {
        public record GetProductsByCategoryResponse(IEnumerable<Product> Products);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products/Category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductsByCategoryQuery(category));
                var response = result.Adapt<GetProductsByCategoryResponse>();
                return Results.Ok(response);
            })
                .WithName("GetProductsByCategory")
                .WithDescription("Get Products by cat")
                .WithSummary("Get Products by cat")
                .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
