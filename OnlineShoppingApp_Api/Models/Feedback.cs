using System;
using System.Collections.Generic;

namespace OnlineShoppingApp_Api.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int OrderId { get; set; }

    public string? Comment { get; set; }

    public DateTime? FeedbackDate { get; set; }

    public bool? IsGoodFeedback { get; set; }
}
