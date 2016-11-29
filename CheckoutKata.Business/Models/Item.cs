using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Business.Models
{
    public class Item
    {
        public Guid ID { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public Unit Unit { get; set; }
        public List<Offer> Offers { get; set; }

        public override string ToString()
        {
            Offer offer = Offers != null ? Offers.Where(x => x.FromDate <= DateTime.Now && x.ToDate >= DateTime.Now).FirstOrDefault() : null;

            string itemRepresentation = SKU + " | Item Price: " + Price + " | ";
            string offerRepresentation =  offer == null ? (" 1 " + Unit.ToString() + " for " + Price) : offer.ToString();

            return itemRepresentation + offerRepresentation;
        }
    }
}
