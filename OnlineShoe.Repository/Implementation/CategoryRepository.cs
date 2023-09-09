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
    public class CategoryRepository:GenericRepository<Category>,IcategoryRepository
    {
        private readonly ShoeDbContext _context;

        public CategoryRepository(ShoeDbContext context):base(context)
        {
            _context = context;
        }
    }
}
