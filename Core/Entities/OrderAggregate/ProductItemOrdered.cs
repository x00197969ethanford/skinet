using System;

namespace Core.Entities.OrderAggregate;

public class ProductItemOrdered
{
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string PictrureUrl { get; set; }
}
