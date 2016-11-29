using CheckoutKata.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Business
{
    public class Checkout
    {
        public Guid ID { get; set; }
        public Sale Sale { get; set; }

        private decimal Total { get; set; }

        public List<Item> SoldItems { get; set; }
        public bool sell(Item item)
        {
            SoldItems.Add(item);

            return true;
        }

        public bool sell(List<Item> items)
        {
            if (SoldItems == null)
            {
                SoldItems = new List<Item>();
            }

            SoldItems.AddRange(items);

            return true;
        }

        public decimal getTotal()
        {
            var groupedItems = SoldItems.GroupBy(x => x.SKU);

            foreach (var itemGroup in groupedItems)
            {
                var item = itemGroup.FirstOrDefault();
                Offer offer = item.Offers != null ? item.Offers.Where(x => x.FromDate <= DateTime.Now && x.ToDate >= DateTime.Now).FirstOrDefault() : null;
                double s = offer != null ? itemGroup.Count() % offer.Quantity : -1;
                if (s == 0) // if number of items is divisible by number of quantity of offer with this items ex: 3 As which means 120.
                {
                    decimal t = ((int)(itemGroup.Count() / offer.Quantity)) * offer.Price;
                    Total += t;
                }
                else if (s != 0 && offer != null) // if number of items is not divisible by number of quantity of offer with this items ex: 4 A which means 3 for 120 and 1 for 50.
                {
                    decimal t1 = ((int)(itemGroup.Count() / offer.Quantity)) * offer.Price;
                    decimal t2 = (decimal)s * item.Price;
                    Total += t1 + t2;
                }
                else // if item is not in any offer.
                {
                    decimal t = itemGroup.Count() * item.Price;
                    Total += t;
                }
            }
            return Total;
        }

        public string getSoldItemsStringRepresentation()
        {
            var groupedItems = SoldItems.GroupBy(x => x.SKU);
            string toReturn = "";

            foreach (var itemGroup in groupedItems)
            {
                var item = itemGroup.FirstOrDefault();
                Offer offer = item.Offers != null ? item.Offers.Where(x => x.FromDate <= DateTime.Now && x.ToDate >= DateTime.Now).FirstOrDefault() : null;
                double s = offer != null ? itemGroup.Count() % offer.Quantity : -1;
                if (s == 0) // if number of items is divisible by number of quantity of offer with this items ex: 3 As which means 120.
                {
                    decimal t = ((int)(itemGroup.Count() / offer.Quantity)) * offer.Price;
                    toReturn += item.SKU + " | Price: " + t + " | " + " Quantity: " + itemGroup.Count() + " | Special Offer: " + offer.ToString() + Environment.NewLine;
                }
                else if (s != 0 && offer != null)  // if number of items is not divisible by number of quantity of offer with this items ex: 4 A which means 3 for 120 and 1 for 50.
                {
                    decimal t1 = ((int)(itemGroup.Count() / offer.Quantity)) * offer.Price;
                    decimal t2 = (decimal)s * item.Price;

                    toReturn += item.SKU + " | Price: " + t1 + " | " + " Quantity: " + (int)(itemGroup.Count() - itemGroup.Count() % offer.Quantity) + " | Special Offer: " + offer.ToString() + Environment.NewLine;
                    toReturn += item.SKU + " | Price: " + t2 + " | " + " Quantity: " + itemGroup.Count() % offer.Quantity + Environment.NewLine;
                }
                else  // if item is not in any offer.
                {
                    decimal t = itemGroup.Count() * item.Price;

                    toReturn += item.SKU + " | Price: " + t + " | " + " Quantity: " + itemGroup.Count() + Environment.NewLine;
                }
            }
            return toReturn;
        }
    }

    
}
