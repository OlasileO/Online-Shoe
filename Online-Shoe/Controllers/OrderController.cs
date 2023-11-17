using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Shoe.Service;
using OnlineShoe.Model;
using OnlineShoe.Repository.Abstract;
using OnlineShoe.Repository.Implementation;
using System.Security.Claims;

namespace Online_Shoe.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IorderRepository _orderRepository;
        private readonly IShoeRepository _shoeRepository;
        private readonly Shoppingcart _shoppingcart;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IorderRepository orderRepository,
            IShoeRepository shoeRepository,
            Shoppingcart shoppingcart,
            ILogger<OrderController> logger)
        {
            _orderRepository = orderRepository;
            _shoeRepository = shoeRepository;
            _shoppingcart = shoppingcart;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("allorder")]
        public async Task<IActionResult> Allorder()
        {

            string userId = UserRepository.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User not authenticated");
            }
            string  userRole = User.FindFirstValue(ClaimTypes.Role);
            var allorders = await _orderRepository.GetOrderByUserIdAndRoleAsync(userId,userRole);
            return Ok(allorders);
        }


        [HttpPost("addItemToShoppingCart")]
        public async Task<IActionResult> AddItemToShoppingCart([FromQuery] int id, [FromQuery] int quantity = 1)
        {
            var shoe = await _shoeRepository.GetById(id);

            if (shoe != null)
            {
                for (int i = 0; i < quantity; i++)
                {
                    _shoppingcart.AddItemtocart(shoe);
                }
            }

            return Ok();
        }

        [Authorize]
        [HttpPost("completeOrder")]
        public async Task<IActionResult> CompleteOrder()
        {

            var items = _shoppingcart.GetshoppingCartItems();
            if (items == null || !items.Any())
            {
                return BadRequest("Shopping cart is empty");
            }

            string userId = UserRepository.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User not authenticated");
            }

            try
            {
                // Store the order
                await _orderRepository.StoreOrderAysnc(items, userId);

                // Clear the shopping cart
                await _shoppingcart.clearShoppingCartAsync();

                // Return the created order
                return Ok("Order completed successfully");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "An error occurred while completing the order.");
            }

        }
    }
}

