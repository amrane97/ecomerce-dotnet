namespace Catalog.API.Models.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<IEnumerable<Product>>;
    public class GetProductByCategoryQueryHandler(IDocumentSession session) 
        : IRequestHandler<GetProductByCategoryQuery, IEnumerable<Product>>
    {
        public async Task<IEnumerable<Product>> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Category))
                .ToListAsync();
                
            return products;
        }
    }
}
