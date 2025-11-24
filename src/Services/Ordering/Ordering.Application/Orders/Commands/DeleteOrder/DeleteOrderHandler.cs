using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Exceptions;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.Id);
        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);
        if (order is null)
        {
            throw new NotFoundOrderException(command.Id);
        }

        dbContext.Orders.Remove(order);
        return new DeleteOrderResult(true);
    }
}
