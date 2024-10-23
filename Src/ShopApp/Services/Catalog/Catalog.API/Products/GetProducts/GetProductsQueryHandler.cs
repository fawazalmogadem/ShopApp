

using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNamber=1,int? PageSize = 10):IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    public class GetProductsQueryHandler(IDocumentSession documentSession) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var result = await documentSession.Query<Product>()
                .ToPagedListAsync(query.PageNamber ?? 1, query.PageSize ?? 10,cancellationToken);
            return new GetProductsResult(result);
        }
    }
}
