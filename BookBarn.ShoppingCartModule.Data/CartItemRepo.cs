using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarn.ShoppingCartModule.Domain.Entities;
using BookBarn.ShoppingCartModule.Domain.Repos;

namespace BookBarn.ShoppingCartModule.Data
{
    public class CartItemRepo : ICartItemRepo
    {
        private readonly CartDbContext db;
        public CartItemRepo(CartDbContext db) { this.db = db; }
        public async Task AddCartItem(CartItem cartItem)
        {
            try
            {
                await db.CartItems.AddAsync(cartItem);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("CartItem could not be added in Database",ex);
            }
        }

        public async Task<CartItem> GetCartItem(int id)
        {
            var cartItem=await db.CartItems.FindAsync(id);
            if(cartItem!=null)
            {
                return cartItem;
            }
            else
            {
                throw new Exception("Cart item Not found");
            }
        }

        public async Task RemoveCartItem(int id)
        {
            var cartItem = await db.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                db.CartItems.Remove(cartItem);
                await db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Cart item Not found");
            }
        }

        public async Task UpdateCartItem(CartItem cartItem)
        {
            try
            {
                db.CartItems.Update(cartItem);
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            { 
                throw new Exception("CartItem could not be Updated in DataBase",ex);
            }
            
        }
    }
}
