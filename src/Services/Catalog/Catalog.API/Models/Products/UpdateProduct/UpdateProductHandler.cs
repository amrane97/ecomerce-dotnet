namespace Catalog.API.Models.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, decimal Price, string ImageFile)
        : ICommand<UpdateProductCommandResponse>;

    public record UpdateProductCommandResponse(bool success);
    public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) 
        : ICommandHandler<UpdateProductCommand, UpdateProductCommandResponse>
    {
        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id);
            if (product is null)
            {
                throw new ProductNotFoundException();
            }
            
            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductCommandResponse(true);
        }
    }
}
