using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Business.Models
{
    public class Sale
    {
        public Guid ID { get; set; }

        private List<Checkout> ChekoutSales { get; set; }

        public Checkout addCheckout(Checkout chk)
        {
            if (ChekoutSales == null)
            {
                ChekoutSales = new List<Checkout>();
            }

            ChekoutSales.Add(chk);

            return chk;
        }

        public bool addCheckouts(List<Checkout> chks)
        {
            if (ChekoutSales == null)
            {
                ChekoutSales = new List<Checkout>();
            }

            ChekoutSales.AddRange(chks);

            return true;
        }

        public List<Checkout> getCheckouts()
        {
            return this.ChekoutSales;
        }
    }
}
