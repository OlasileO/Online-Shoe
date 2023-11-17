using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShoe.Model;
using OnlineShoe.Model.Data;
using OnlineShoe.Model.Migrations;
using OnlineShoe.Repository.Abstract;
using System.Security.Claims;

namespace OnlineShoe.Repository.Implementation
{
    public class OrderRepository : GenericRepository<Order>, IorderRepository
    {
        private readonly ShoeDbContext _Context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private ClaimsPrincipal User;

        public OrderRepository(ShoeDbContext context, UserManager<AppUser> userManager, 
            IHttpContextAccessor httpContextAccessor):base(context)
        {
            _Context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        
        public async Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userId,string userRole)
        {
           

            var orders = await _Context.Orders.Include(o => o.orderItems)
                                                .ThenInclude(n => n.Shoe)
                                                .Include(u=> u.User)
                                                .ToListAsync();
            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.Userid == userId).ToList();
            }
            return orders;
        }

        public async Task StoreOrderAysnc(List<ShoppingCartItems> items, string userId)
        {
          
            var order = new Order()
            {
                Userid = userId,
                Order_date = DateTime.Now,
            };

            await _Context.Orders.AddAsync(order);
            await _Context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderitem = new Orderitem()
                {
                    quantity = item.Quatity,
                    ShoeId = item.Shoe.Id,
                    OrderId = order.Id,
                    Totalprice = item.Shoe.Price * item.Quatity,
                };
                await _Context.OrderItems.AddAsync(orderitem);
            }
           
      
            await _Context.SaveChangesAsync();
            
        }
        
    }
}
