

namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string username,CancellationToken token);
    Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken token);
    Task<bool> DeleteBasket(string username, CancellationToken token);
}

