using Microsoft.EntityFrameworkCore;
using Modules.Basket.Contract.Services;
using Modules.Baskets.Contract.DTOs.BasketItemDTOs;
using Modules.Baskets.Domain;
using Modules.Baskets.Infrastructure.Persistence;
using Modules.Products.Contracts.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Exceptions;
using Microsoft.Extensions.Logging;

namespace Modules.Baskets.Infrastructure.Service
{
    public class BasketModuleService : IBasketModuleService
    {
    
        private readonly BasketDbContext _basketDbContext;
        private readonly IProductModuleService _productModuleService;
        private readonly ILogger<BasketModuleService> _logger;

        public BasketModuleService(BasketDbContext basketdbcontext, IProductModuleService productModuleService, ILogger<BasketModuleService>     logger  )
        {
            _basketDbContext = basketdbcontext;
            _productModuleService = productModuleService;
            _logger = logger;
        }

        public async Task AddItemToBasketAsync(string userId, RequestBasketItem requestBasketItem)
        {
            var basket = await _basketDbContext.Baskets
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (basket == null)
            {
                basket = new Domain.Basket { UserId = userId };
                _basketDbContext.Add(basket);
            }


            var existingItem = basket.Items.FirstOrDefault(i => i.ProductId == requestBasketItem.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += requestBasketItem.Quantity;
            }
            else
            {
                basket.Items.Add(new BasketItem
                {
                    ProductId = requestBasketItem.ProductId,
                    Quantity = requestBasketItem.Quantity,
                });
            }

            await _basketDbContext.SaveChangesAsync();
            _logger.LogInformation
                ("Basket item added. UserId: {UserId}, ProductId: {ProductId}, Quantity: {Quantity}",
                userId, requestBasketItem.ProductId, requestBasketItem.Quantity
                );
        }

        public async Task<List<BasketItemDtos>> GetBasketAsync(string userId)
        {
            var basket = await _basketDbContext.Baskets
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (basket == null)
                return new List<BasketItemDtos>();
         

            var ids = basket.Items.Select(i => i.ProductId).ToList();

            if (!ids.Any())
            {
                return new List<BasketItemDtos>();
            }

            var producttNames = await _productModuleService.GetProductNamesByIdsAsync(ids);

            var basketitemsdto = basket.Items.Select(i => new BasketItemDtos
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = producttNames.ContainsKey(i.ProductId) ? producttNames[i.ProductId].Price : 0,
                ProductName = producttNames.ContainsKey(i.ProductId) ? producttNames[i.ProductId].Name : "Məhsul tapılmadı"
            }).ToList();

            return basketitemsdto;
        }

        public async Task RemoveItemFromBasketAsync(string userId, int productId)
        {
            var basket = await _basketDbContext.Baskets
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.UserId == userId);

            if (basket == null)
                throw new NotFoundException("Səbət tapılmadı");
          
            var itemToRemove = basket.Items.FirstOrDefault(i => i.ProductId == productId);

            if (itemToRemove == null)
                throw new NotFoundException($"ID-si {productId} olan məhsul səbətdə tapılmadı");

           
            basket.Items.Remove(itemToRemove);

            await _basketDbContext.SaveChangesAsync();
        }
    }
}