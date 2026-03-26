using System;
using System.Collections.Generic;

namespace OnlineShoppingApp_Api.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
}
