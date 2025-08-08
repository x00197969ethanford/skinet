using System;
using Core.Entities;

namespace Core.interfaces;

public interface IPaymentService
{
    Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId);

    Task<string> RefundPayment(string payentIntentId);
}
