namespace Catalog.API.Models.Products.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResponse>;
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
        : IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>
    {
        public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await session
                .LoadAsync<Product>(query.ProductId, cancellationToken);

            if (product is null) 
            {
                throw new ProductNotFoundException();
            }

            return new GetProductByIdResponse(product);
        }
    }
}
