using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.EndPoints
{
    public class CreateOrder : ICarterModule
    {
        public record CreateOrderRequest(OrderDto Order);
        public record CreateOrderResponse(Guid Id);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                var CreateOrderComman = request.Adapt<CreateOrderCommand>();
                var _result = await sender.Send(CreateOrderComman);
                var _response = _result.Adapt<CreateOrderResponse>();
                return Results.Created($"/orders/{_response.Id}", _response);
            })
              .WithName("CreateOrder")
              .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Create Order")
              .WithDescription("Create Order");
        }
    }
}
