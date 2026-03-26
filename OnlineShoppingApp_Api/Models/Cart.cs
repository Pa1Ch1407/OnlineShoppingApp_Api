using System;
using System.Collections.Generic;

namespace OnlineShoppingApp_Api.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}
