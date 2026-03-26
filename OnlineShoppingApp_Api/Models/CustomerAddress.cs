using System;
using System.Collections.Generic;

namespace OnlineShoppingApp_Api.Models;

public partial class CustomerAddress
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public int PostalCode { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
}
