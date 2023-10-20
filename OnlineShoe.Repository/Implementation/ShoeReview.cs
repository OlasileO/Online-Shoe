using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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
    public class ShoeReview : GenericRepository<Shoe_Review>,IShoeReview
    {
        private readonly ShoeDbContext _Context;

        public ShoeReview(ShoeDbContext context):base(context)
        {
            _Context = context;
        }
    }
}
