using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarn.ShoppingCartModule.Domain.Entities;
using BookBarn.ShoppingCartModule.Domain.Repos;
using Microsoft.EntityFrameworkCore;

namespace BookBarn.ShoppingCartModule.Data
{
    public class CartRepo : ICartRepo
    {
        private readonly CartDbContext db;
        public CartRepo(CartDbContext db) { this.db = db; }
        public async Task AddNewCart(Cart cart)
        {
            try
            {
                await db.Carts.AddAsync(cart);
                await db.SaveChangesAsync();
            }
            catch(Exception ex) 
            {
                throw new Exception("New Cart could not be added to database",ex);
            }
            
        }
        public async Task<Cart> GetActiveCartByUserId(string UserId)
        {
            var UserActiveCart = await db.Carts.Where(c => c.UserId==UserId && c.IsCartActive==true).Include(c => c.CartItems).FirstOrDefaultAsync();
            if (UserActiveCart != null)
            {
                return UserActiveCart;
            }
            else
            {
                throw new Exception("New Active Cart Found For User");
            }
        }

        public async Task DeleteCart(int id)
        {
            var cart= await db.Carts.FindAsync(id);
            if (cart != null)
            {
                db.Remove(cart);
                await db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No Cart Found with this id to delete");  
            }
        }
        public async Task<Cart> GetCartById(int id)
        {
            var cart = await db.Carts.Where(c=>c.CartId==id).Include(c=>c.CartItems).FirstOrDefaultAsync();
            if(cart != null)
            {
                return cart;
            }
            else
            { 
                throw new Exception("No Cart Found with this id"); 
            }
        }

        public async Task UpdateActiveStatus(int id)
        {
            var cart = await db.Carts.FindAsync(id);
            if (cart != null) 
            { 
                cart.IsCartActive=!cart.IsCartActive;
            }
            else
            {
                throw new Exception("No Cart Found with this id to update");
            }
        }
    }
}
