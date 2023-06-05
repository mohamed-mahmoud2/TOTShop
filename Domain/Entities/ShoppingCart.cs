﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public Decimal TotalCost { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}