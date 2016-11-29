using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Business.Models
{
    public class Offer
    {
        public Guid ID { get; set; }
        public Item Item { get; set; }        
        public double Quantity { get; set; }
        public Unit Unit { get; set; }
        public decimal Price { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public override string ToString()
        {
            return (Quantity + " " + Unit.ToString() + " for " + Price);
        }
    }
}
