using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarn.ShoppingCartModule.Domain.Entities
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        [ForeignKey("Cart")]
        public int CartId {  get; set; }
        public int BookId {  get; set; }
        [Range(1, 100)]
        public int Quantity { get; set; } 

    }
}
