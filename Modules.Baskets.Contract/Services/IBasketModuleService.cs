using Modules.Baskets.Contract.DTOs.BasketItemDTOs;

namespace Modules.Basket.Contract.Services;

public interface IBasketModuleService
{
    Task<List<BasketItemDtos>> GetBasketAsync(string userId);
    Task AddItemToBasketAsync(string userId, RequestBasketItem requestBasketItem);
    Task RemoveItemFromBasketAsync(string userId, int productId);
}
