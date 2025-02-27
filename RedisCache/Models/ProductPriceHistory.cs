using System;
using System.Collections.Generic;

namespace RedisCache.Models;

public partial class ProductPriceHistory
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public decimal OldPrice { get; set; }

    public decimal NewPrice { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
