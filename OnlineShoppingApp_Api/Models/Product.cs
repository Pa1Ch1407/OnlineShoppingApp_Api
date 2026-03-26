using System;
using System.Collections.Generic;

namespace OnlineShoppingApp_Api.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
}
