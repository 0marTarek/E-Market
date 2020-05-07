using EEMarket.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EEMarket.context
{
    public class DB_CONTEXT : DbContext
    {
        public DbSet<product> product { get; set; }
        public DbSet<Category> category { get; set; }

        public DbSet<Cart> cart { get; set; }
    }
}