using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository _repository, IDistributedCache cache) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var cacheBasket = await cache.GetStringAsync(userName, cancellationToken);

        if (!string.IsNullOrEmpty(cacheBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket);
        }

        var basket = await _repository.GetBasket(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        await _repository.StoreBasket(cart, cancellationToken);

        await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart), cancellationToken);

        return cart;
    }
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {   
        await _repository.DeleteBasket(userName, cancellationToken);

        await cache.RemoveAsync(userName, cancellationToken);

        return true;
    }
}
