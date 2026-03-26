using System;
using System.Collections.Generic;

namespace OnlineShoppingApp_Api.Models;

public partial class OrderPayment
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string? PaymentMethod { get; set; }

    public int TransactionId { get; set; }

    public double Amount { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime PaymentDate { get; set; }
}
