﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GardenyaGirisimciKadinlar.Models
{
    public class ShoppingCardViewModel
    {
        public List<Cart> CartItems { get; set; }

        public decimal CartTotal { get; set; }

    }
}