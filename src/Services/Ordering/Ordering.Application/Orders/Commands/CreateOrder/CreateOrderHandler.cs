using BuildingBlocks.CQRS;
using MediatR;
using Ordering.Application.Data;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext dbContext) :
    ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    async Task<CreateOrderResult> IRequestHandler<CreateOrderCommand, CreateOrderResult>.Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);


        return new CreateOrderResult(order.Id.Value);
    }

    private Order CreateNewOrder(OrderDto order)
    {
        var shippingAddress = Address.Of(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode);
        var billingAddress = Address.Of(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress, order.ShippingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode);

        var newOrder = Order.Create(
            orderId: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(order.CustomerId),
            orderName: OrderName.Of(order.OrderName),
            shippingAddress,
            billingAddress,
            Payment.Of(order.Payment.CardName, order.Payment.CardNumber, order.Payment.DateExpire, order.Payment.Cvv, 1));

        foreach (var orderItem in order.OrderItems)
        {
            newOrder.Add(ProductId.Of(orderItem.ProductId), orderItem.Quantity, orderItem.Price);
        }

        return newOrder;
    }
}
