﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Diamond.Data.Models;

public partial class Productcategory
{
    public string CategoryId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Diamond> Diamonds { get; set; } = new List<Diamond>();
}