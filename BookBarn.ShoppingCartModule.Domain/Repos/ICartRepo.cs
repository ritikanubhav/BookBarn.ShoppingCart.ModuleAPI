using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarn.ShoppingCartModule.Domain.Entities;

namespace BookBarn.ShoppingCartModule.Domain.Repos
{
    public interface ICartRepo
    {
        public Task<Cart> GetCartById(int id);
        public Task<Cart> GetActiveCartByUserId(string UserId);
        public Task AddNewCart(Cart cart);
        public Task UpdateActiveStatus(int id);
        public Task DeleteCart(int id);
    }
}
