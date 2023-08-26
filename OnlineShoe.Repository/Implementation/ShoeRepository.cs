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
    public class ShoeRepository:GenericRepository<Shoe>,IShoeRepository
    {
        private readonly ShoeDbContext _Context;

        public ShoeRepository(ShoeDbContext context):base(context) 
        {
            _Context = context;
        }
    }
}
