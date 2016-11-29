using CheckoutKata.Business;
using CheckoutKata.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCheckout.View
{
    class Program
    {
        static void Main(string[] args)
        {
            Sale sale = new Sale();
            Checkout checkout = new Checkout();
            checkout.ID = Guid.NewGuid();
            checkout.Sale = sale;

            ModelCollection mc = ModelCollection.Instance;

            List<Item> items = new List<Item>();

            Item A = new Item { ID = Guid.NewGuid(), Name = "A", SKU = "A", Price = 50, Unit = Unit.Kg };
            Item B = new Item { ID = Guid.NewGuid(), Name = "B", SKU = "B", Price = 30, Unit = Unit.Pcs };
            Item C = new Item { ID = Guid.NewGuid(), Name = "B", SKU = "C", Price = 60, Unit = Unit.Pcs };
            Item D = new Item { ID = Guid.NewGuid(), Name = "D", SKU = "D", Price = 15, Unit = Unit.Pcs };

            A.Offers = new List<Offer>();
            B.Offers = new List<Offer>();
            A.Offers.Add(new Offer { ID = Guid.NewGuid(), Item = A, FromDate = DateTime.Now.AddDays(-1), ToDate = DateTime.Now.AddDays(7), Price = 120, Quantity = 3, Unit = Unit.Kg  });
            B.Offers.Add(new Offer { ID = Guid.NewGuid(), Item = B, FromDate = DateTime.Now.AddDays(-1), ToDate = DateTime.Now.AddDays(7), Price = 45, Quantity = 2, Unit = Unit.Pcs });

            // Four As; Two Bs; Two Cs; One D; A and B have valid offers while C and D do not have any valid offer
            items.Add(A);
            items.Add(B);
            items.Add(B);
            items.Add(C);
            items.Add(C);
            items.Add(D);
            items.Add(A);
            items.Add(A);
            items.Add(A);

            mc.addItems(items);

            bool success = checkout.sell(mc.getItems());
            
            if (success)
            {
                sale.addCheckout(checkout);
                Checkout chk = sale.getCheckouts().LastOrDefault();
                decimal total = chk.getTotal();
                string stringRepresentation = chk.getSoldItemsStringRepresentation();

                Console.WriteLine(stringRepresentation);

                Console.WriteLine("Press enter to close...");
                Console.ReadLine();
            }

            
        }
    }
}
