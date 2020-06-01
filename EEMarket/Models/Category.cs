using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEMarket.Models
{
    public partial class Category
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Number_of_product { get; set; }
    }
}