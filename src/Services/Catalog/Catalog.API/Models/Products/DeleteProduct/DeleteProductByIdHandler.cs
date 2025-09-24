
namespace Catalog.API.Models.Products.DeleteProduct
{
    public record DeleteProductByIdCommand(Guid Id) : ICommand<DeleteProductByIdCommandResponse>;
    public record DeleteProductByIdCommandResponse(bool success);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductByIdCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is Required");
        }
    }
    public class DeleteProductByIdQueryHandler(IDocumentSession session)
        : IRequestHandler<DeleteProductByIdCommand, DeleteProductByIdCommandResponse>
    {
        public async Task<DeleteProductByIdCommandResponse> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id);
            if(product is null)
            {
                throw new ProductNotFoundException(command.Id);
            }
            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductByIdCommandResponse(true);
        }
    }
}
