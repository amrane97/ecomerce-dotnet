
namespace Catalog.API.Models.Products.GetProducts
{
    public record GetProductsQuery(): IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    public class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) : IRequestHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session
                .Query<Product>()
                .ToListAsync(cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
