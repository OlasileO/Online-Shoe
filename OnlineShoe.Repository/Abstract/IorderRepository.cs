using OnlineShoe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Repository.Abstract
{
    public interface IorderRepository:IGenericRepository<Order>
    {
        Task StoreOrderAysnc(List<ShoppingCartItems> items, string userId);

        Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userId, string userRole);
       
    }
}
