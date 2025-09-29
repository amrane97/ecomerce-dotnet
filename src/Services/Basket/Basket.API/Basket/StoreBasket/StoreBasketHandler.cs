using Discount.Grpc;
using JasperFx.Events.Daemon;
using NetTopologySuite.Index.HPRtree;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);
public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotEmpty().WithMessage("Cart can not be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
        }
}

public class StoreBasketCommandHandler
    (IBasketRepository _repository, DiscountProtoService.DiscountProtoServiceClient discountProto) 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);
        ShoppingCart cart = command.Cart;

        // TODO: update cache
        await _repository.StoreBasket(cart, cancellationToken);

        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        // TODO : communicate with discount.grpc 
        foreach (var item in cart.Items)
        {
            var coupon = discountProto.GetDiscount(new GetDiscountRequest { ProductName = item.ProductName });
            item.Price -= coupon.Amount;
        }
    }
}
