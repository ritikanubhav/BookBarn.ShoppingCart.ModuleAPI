using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarn.ShoppingCartModule.Domain.Entities
{
    public class Cart
    {
        public int CartId { get; set; }
        public string UserId {  get; set; }
        public List<CartItem>? CartItems { get; set; }=new List<CartItem>();
        public bool IsCartActive {  get; set; } =true;
    }
}
