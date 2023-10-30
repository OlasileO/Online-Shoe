using OnlineShoe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Repository.Abstract
{
    public interface IorderRepository
    {
        Task StoreOrderAysnc(List<ShoppingCart> items, string userId);

        Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userRole);
    }
}
