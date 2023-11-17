using OnlineShoe.Model.Data;
using OnlineShoe.Model;
using Microsoft.EntityFrameworkCore;

namespace Online_Shoe.Service
{
    public class Shoppingcart
    {

        private readonly ShoeDbContext _Context;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItems> shoppingCartItems { get; set; }

        public Shoppingcart(ShoeDbContext dbcontext)
        {
            _Context = dbcontext;
        }
        public static Shoppingcart GetShoppingCart(IServiceProvider serviceProvider)
        {
            ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = serviceProvider.GetService<ShoeDbContext>();

            string cartId = session.GetString("cartId") ?? Guid.NewGuid().ToString();
            session.SetString("cartId", cartId);

            return new Shoppingcart(context) { ShoppingCartId = cartId };
        }

        public void AddItemtocart(Shoe shoe)
        {
            
                var shoppingCarts = _Context.ShoppingCarts.FirstOrDefault(b => b.Shoe.Id == shoe.Id
                                                        && b.shoppingId == ShoppingCartId);
                if (shoppingCarts == null)
                {
                    shoppingCarts = new ShoppingCartItems
                    {
                        shoppingId = ShoppingCartId,
                        Shoe = shoe,
                        Quatity = 1

                    };
                    _Context.ShoppingCarts.Add(shoppingCarts);
                }
                else
                {
                    shoppingCarts.Quatity++;
                }
            
            _Context.SaveChanges();
        }

        public List<ShoppingCartItems> GetshoppingCartItems()
        { 
               var shoppingCartItems = _Context.ShoppingCarts
                    .Where(s => s.shoppingId == ShoppingCartId)
                    .Include(b => b.Shoe)
                    .ToList();
            if (shoppingCartItems == null)
            { return null!; }
            return shoppingCartItems;
        }
        public double GetshoppingCartTotal()
        {
            var total = _Context.ShoppingCarts
        .Where(s => s.shoppingId == ShoppingCartId)
        .Select(b => b.Shoe.Price * b.Quatity)
        .Sum();
           
            return total;
        }

        public async Task clearShoppingCartAsync()
        {
            var item = await _Context.ShoppingCarts.Where(s => s.shoppingId == ShoppingCartId).ToListAsync();
            _Context.ShoppingCarts.RemoveRange(item);
            await _Context.SaveChangesAsync();
        }
    }
}
