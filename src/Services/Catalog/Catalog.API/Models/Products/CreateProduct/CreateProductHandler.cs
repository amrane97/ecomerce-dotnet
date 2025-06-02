using BuildingBlocks.CQRS;
using MediatR;

namespace Catalog.API.Models.Products.CreateProduct
{

    public record CreateProductCommand(string Name, List<string> Category, string Description, decimal price, string ImageFile) 
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {

        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Create product entity from command object
            var product = new Product 
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.price
            };
            // Save the database

            // return CreateProductResult result
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
