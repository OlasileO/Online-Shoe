using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Model
{
    public class PaymentDetails
    {
        public int Id { get; set; }
        public Order? Order { get; set; }
        public int OrderId { get; set; }
        public int CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime  ExpiryDate { get; set; }
        public int CVV { get; set; }    
        public string PaymentStatus { get; set; }
    }
}
