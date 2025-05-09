using System;
using Core.Entities;
using Core.interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CartController(ICartService cartService) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<ShoppingCart>> GetCartById(string id)
    {
        var cart = await cartService.GetCartAsync(id);

        return Ok(cart ?? new ShoppingCart{Id = id});
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
    {
        var UpdatedCart = await cartService.SetCartAsync(cart);

        if (UpdatedCart == null) return BadRequest("Problem with cart");

        return UpdatedCart;
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteCart(string id)
    {
        var result = await cartService.DeleteCartAsync(id);

        if (!result) return BadRequest("Problem deleting cart");

        return Ok();
    }
}
