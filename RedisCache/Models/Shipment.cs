using System;
using System.Collections.Generic;

namespace RedisCache.Models;

public partial class Shipment
{
    public int ShipmentId { get; set; }

    public int? OrderId { get; set; }

    public int? ShipperId { get; set; }

    public DateTime? ShipmentDate { get; set; }

    public string? TrackingNumber { get; set; }

    public string? Status { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Shipper? Shipper { get; set; }
}
