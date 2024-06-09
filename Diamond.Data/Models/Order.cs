﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Diamond.Data.Models;

public partial class Order
{
    public string OrderId { get; set; }

    public string CustomerId { get; set; }

    public DateTime Date { get; set; }

    public string PaymentMethod { get; set; }

    public string ShippingAddress { get; set; }

    public decimal TotalPrice { get; set; }

    public string PaymentStatus { get; set; }

    public string ShippingStatus { get; set; }

    public string PromotionId { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    public virtual Promotion Promotion { get; set; }
}