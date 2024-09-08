using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarn.ShoppingCartModule.Domain.Entities;

namespace BookBarn.ShoppingCartModule.Domain.Repos
{
    public interface ICartItemRepo
    {
        public Task AddCartItem(CartItem cartItem);
        public Task<CartItem> GetCartItem(int id);
        public Task RemoveCartItem(int id);
        public Task UpdateCartItem(CartItem cartItem);

    }
}
