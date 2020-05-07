using EEMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEMarket.ViewModel
{
    public class ProductCategoryView
    {
        public product Product { get; set; }

        public IEnumerable<Category> category { get; set; }
    }
}