using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model
{
    public class ShippingInfo
    {
        public int Id { get; set; }
        public Order? Order { get; set; }
        public int OrderId { get; set; }
        public string RecipeintName { get; set; }
        public  string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string  ShippingStatus {  get; set; }

    }
}
