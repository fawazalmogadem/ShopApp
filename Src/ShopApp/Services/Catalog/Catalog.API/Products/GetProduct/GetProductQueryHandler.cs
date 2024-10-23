using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProduct
{
    public record GetProductQuery(Guid Id) : IQuery<GetProductResult>;
    public record GetProductResult(Product Products);
    public class GetProductQueryHandler(IDocumentSession documentSession) : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var product = await documentSession.LoadAsync<Product>(query.Id);
            //if (product == null) {
            //    throw new ProductNotFoundException();
            //}
            return new GetProductResult(product!);
        }
    }
}
