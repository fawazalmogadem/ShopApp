
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string username, CancellationToken token)
        {
            var deleted = await basketRepository.DeleteBasket(username, token);
            if(deleted)
                await cache.RemoveAsync(username);
            return deleted;
        }

        public async Task<ShoppingCart> GetBasket(string username, CancellationToken token)
        {
            var cachedBasked = await cache.GetStringAsync(username);
            if (!string.IsNullOrEmpty(cachedBasked))
            {
                var basket = JsonSerializer.Deserialize<ShoppingCart>(cachedBasked);
                return basket!;
            }
            else
            {
                var basket = await basketRepository.GetBasket(username, token);
                await cache.SetStringAsync(username, JsonSerializer.Serialize(basket));
                return basket;
            }
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken token)
        {
            var basket = await basketRepository.StoreBasket(cart, token);
            if (basket != null) {
                await cache.SetStringAsync(basket.UserName,JsonSerializer.Serialize(basket));
            }
            return basket!;
        }
    }
}
