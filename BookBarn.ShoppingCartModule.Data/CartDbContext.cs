using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarn.ShoppingCartModule.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookBarn.ShoppingCartModule.Data
{
    public class CartDbContext:DbContext
    {
        /// <summary>
        /// Configuring Sql Server Connection using connection String from Config File
        /// </summary>
        /// <param name="options"></param>
        public CartDbContext(DbContextOptions<CartDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
