using System.Security.Claims;
using BookBarn.ShoppingCartModule.Domain.Entities;
using BookBarn.ShoppingCartModule.Domain.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBarn.ShoppingCart.ModuleAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepo cartRepo;
        public CartController(ICartRepo cartRepo) { this.cartRepo = cartRepo; }

        // ...api/Cart/{id}
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> GetCartById(int id)
        {
            try
            {
                var item = await cartRepo.GetCartById(id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ...api/Cart/user
        [HttpGet]
        [Route("user")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> GetCartForUser()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // or any other claim type
                if (userId != null)
                {
                    var item = await cartRepo.GetActiveCartByUserId(userId);
                    return Ok(item);
                }
                else
                {
                    return BadRequest("UserId not ound in token");
                }
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ...api/Cart/{id}
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> RemoveCart(int id)
        {
            try
            {
                await cartRepo.DeleteCart(id);
                return Ok(" removed Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ...api/Cart
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> CreateCart(Cart cart)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // or any other claim type
                if (userId != null)
                {
                    cart.UserId = userId;
                }
                await cartRepo.AddNewCart(cart);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ...api/Cart/{id}

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> UpdateCartActiveStatus(int id)
        {
            try
            {
                var cart= await cartRepo.GetCartById(id);
                if(cart != null) 
                    await cartRepo.UpdateActiveStatus(id);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
