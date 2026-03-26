using System;
using System.Collections.Generic;

namespace OnlineShoppingApp_Api.Models;

public partial class CancelOrder
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string? Comment { get; set; }

    public bool IsChargesApply { get; set; }

    public double CancellationCharges { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public bool IsCompleted { get; set; }
}
