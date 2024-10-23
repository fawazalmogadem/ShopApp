
namespace Catalog.API.Products.CreateProduct
{
    public class CreateProductValidtor : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidtor()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be greater than zero");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");

        }
    }
    public record CreateProductCommand(Guid Id, string Name, string Description, List<string> Category, string ImageFile,
    decimal Price) : ICommand<CreateProductResult>
    {

    }
    public record CreateProductResult(Guid Id);
    internal class CreateProductHandler(IDocumentSession documentSession) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            
            var produnct = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Category = command.Category,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            //save to db
            documentSession.Store(produnct);
            await documentSession.SaveChangesAsync();
            return new CreateProductResult(produnct.Id);
        }
    }
}
