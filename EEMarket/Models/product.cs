using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EEMarket.Models
{
    public class product
    {
        [Key]
        public int ID { get; set; }
        public String Name { get; set; }
        public double Price { get; set; }
        public String Image { get; set; }
        public String Description { get; set; }
        public Category category_Id { get; set; }
        public int? CategoryId { get; set; }
        public virtual Cart Cart { get; set; }



    }
}