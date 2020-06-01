using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EEMarket.Models
{
    public class Cart
    {
        [Key]
        public int item_id  { get; set; }

        public virtual product product { get; set; }
        public int ProductId { get; set; }

        public DateTime add_at { get; set; }
        public string card_id { get; set; }
    }
}