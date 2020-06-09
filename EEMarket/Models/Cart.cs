using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EEMarket.Models
{
    public class Cart
    {
        [Key]
        [ForeignKey("product")]
        public int ProductId { get; set; }

        public DateTime add_at { get; set; }
        public virtual product product { get; set; }
    }
}