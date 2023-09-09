using OnlineShoe.Model;
using OnlineShoe.Model.Data;
using OnlineShoe.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Repository.Implementation
{
    public class Shoe_CategoryRepository:GenericRepository<Shoe_Category>,IShoe_CategoryRepository
    {
        private readonly ShoeDbContext _context;

        public Shoe_CategoryRepository(ShoeDbContext context):base(context)
        {
            _context = context;
        }
    }
}
