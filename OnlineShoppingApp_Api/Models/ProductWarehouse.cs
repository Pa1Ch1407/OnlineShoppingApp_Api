using System;
using System.Collections.Generic;

namespace OnlineShoppingApp_Api.Models;

public partial class ProductWarehouse
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int TotalQuantity { get; set; }

    public int AvailableQuantity { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
}
