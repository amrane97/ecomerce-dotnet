using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Exceptions;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext dbContext) :
    ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.OrderDto.Id);
        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);

        if(order is null)
        {
            throw new NotFoundOrderException(command.OrderDto.Id);
        }

        UpdateNewOrder(order, command.OrderDto);
        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpdateOrderResult(true);
    }

    public void UpdateNewOrder(Order order, OrderDto orderDto)
    {
        var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
        var billingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);
        var updatePayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.DateExpire, orderDto.Payment.Cvv, 1);

        order.Update(OrderName.Of(orderDto.OrderName), shippingAddress, billingAddress, updatePayment, orderDto.Status);
    }
}
