using System;
using Core.Entities;
using Core.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PaymentsController(IPaymentService paymentService, IGenericRepository<DeliveryMethod> dmRepo) : BaseApiController
{
    [Authorize]
    [HttpPost("{cartId}")]
    public async Task<ActionResult<ShoppingCart>> CreateOrUpdatePaymentIntent(string cartid)
    {
        var cart = await paymentService.CreateOrUpdatePaymentIntent(cartid);

        if (cart == null) return BadRequest("Problem with your cart");

        return Ok(cart);
    }

    [HttpGet("delivery-methods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
    {
        return Ok(await dmRepo.ListAllAsync());
    }
}
