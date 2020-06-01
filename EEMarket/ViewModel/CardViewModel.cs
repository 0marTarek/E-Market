using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EEMarket.Models;

namespace EEMarket.ViewModel
{
    public class CardViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}