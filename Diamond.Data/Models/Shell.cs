﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Diamond.Data.Models;

public partial class Shell
{
    public string ShellId { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int AmountAvailable { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();
}