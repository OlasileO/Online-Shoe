using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShoe.Model;
using OnlineShoe.Model.Data;
using OnlineShoe.Repository.Abstract;

namespace OnlineShoe.Repository.Implementation
{
    public class OrderRepository : IorderRepository
    {
        private readonly ShoeDbContext _Context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderRepository(ShoeDbContext context, UserManager<AppUser> userManager, 
            IHttpContextAccessor httpContextAccessor)
        {
            _Context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userRole)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User is not logged-in");
            var orders = await _Context.Orders.Include(o=> o.order_Items)
                                                .ThenInclude(n => n.Shoe)
                                                .ToListAsync();

            if (userRole != "admin")
            {
                orders = orders.Where(n => n.User_id == userId).ToList();
            }
            return orders;
        }

        public async Task StoreOrderAysnc(List<ShoppingCart> items, string userId)
        {
            var order = new Order()
            {
                User_id = userId,
                Order_date = DateTime.Now,

            };
            await _Context.Orders.AddAsync(order);
            await _Context.SaveChangesAsync();



            foreach (var item in items)
            {
                var orderitem = new Order_item()
                {
                    quantity = item.Quatity,
                    Shoe_Id = item.Shoe.Id,
                    Order_Id = order.Id,
                    Total_price = item.Shoe.Price,
                };
                await _Context.OrderItems.AddAsync(orderitem);
            }
            await _Context.SaveChangesAsync();
        }
        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
