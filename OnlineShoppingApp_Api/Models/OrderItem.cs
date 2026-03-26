using System;
using System.Collections.Generic;

namespace OnlineShoppingApp_Api.Models;

public partial class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int CartItemId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}
