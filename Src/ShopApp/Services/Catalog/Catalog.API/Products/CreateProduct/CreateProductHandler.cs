using BuildingBlocks.CQRS;
using MediatR;
using Catalog.API.Models;
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand( Guid Id,string Name,string Description ,List<string> Category ,string ImageFile,
    decimal Price) : ICommand<CreateProductResult>
    {

    }
    public record CreateProductResult(Guid id);
    internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
           var produnct =  new Product
           {
                Name = command.Name,
                Description = command.Description,
                Category = command.Category,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
