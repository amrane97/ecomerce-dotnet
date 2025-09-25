namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart cart);

public class GetBasketHandler(IBasketRepository _repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await _repository.GetBasket(query.UserName, cancellationToken);

        return new GetBasketResult(basket);
    }
}
