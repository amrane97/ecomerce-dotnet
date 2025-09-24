namespace Catalog.API.Models.Products.GetProducts
{
    public record GetProductsQuery(int PageNumber = 1, int PageSize = 10): IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    public class GetProductsQueryHandler(IDocumentSession session) : IRequestHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session
                .Query<Product>()
                .ToPagedListAsync(query.PageNumber, query.PageSize, cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
