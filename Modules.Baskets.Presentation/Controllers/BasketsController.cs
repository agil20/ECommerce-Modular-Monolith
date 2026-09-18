using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Basket.Contract.Services;
using Modules.Baskets.Contract.DTOs.BasketItemDTOs;
using System.Security.Claims;

namespace Modules.Baskets.Controllers;

[Authorize]
[ApiExplorerSettings(GroupName = "baskets")]
[Route("api/[controller]")]
[ApiController]
public class BasketsController : ControllerBase
{
    private readonly IBasketModuleService _basketModuleService;

    public BasketsController(IBasketModuleService basketModuleService)
    {
        _basketModuleService = basketModuleService;
    }

    // Token-dəki istifadəçi Id-si — client heç vaxt özü göndərmir
    private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetBasket()
    {
        var basketItems = await _basketModuleService.GetBasketAsync(CurrentUserId);
        return Ok(new ApiResponseModel(true, 200, "Basket retrieved successfully", basketItems));
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItemToBasket([FromBody] RequestBasketItem requestBasketItem)
    {
        await _basketModuleService.AddItemToBasketAsync(CurrentUserId, requestBasketItem);
        return Ok(new ApiResponseModel(true, 200, "Item added to basket successfully"));
    }

    [HttpDelete("items/{productId}")]
    public async Task<IActionResult> RemoveItemFromBasket(int productId)
    {
        await _basketModuleService.RemoveItemFromBasketAsync(CurrentUserId, productId);
        return Ok(new ApiResponseModel(true, 200, "Məhsul səbətdən uğurla silindi"));
    }
}
