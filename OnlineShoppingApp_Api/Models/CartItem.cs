using System;
using System.Collections.Generic;

namespace OnlineShoppingApp_Api.Models;

public partial class CartItem
{
    public int Id { get; set; }

    public int CartId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public double ActualPrice { get; set; }

    public double PurchasedPrice { get; set; }

    public double DiscountedPrice { get; set; }

    public bool Iswishlisted { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}
