using System;
using System.Collections.Generic;

namespace OnlineShoppingApp_Api.Models;

public partial class Order
{
    public int Id { get; set; }

    public int CartId { get; set; }

    public int CustomerId { get; set; }

    public int CustomerAddressId { get; set; }

    public double TotalPurchasedPrice { get; set; }

    public double TotalDiscountPrice { get; set; }

    public double TotalAmount { get; set; }

    public int OrderStatusId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public bool? IsActive { get; set; }
}
