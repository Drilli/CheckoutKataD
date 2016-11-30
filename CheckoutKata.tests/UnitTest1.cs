using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckoutKata.Business.Models;
using CheckoutKata.Business;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata.tests
{
    [TestClass]
    public class UnitTest1
    {
        public ModelCollection ModelCollection = ModelCollection.Instance;
        public Sale Sale { get; set; }

        [TestInitialize]
        public void TestSetUp()
        {
            Sale sale = new Sale();
            sale.ID = Guid.NewGuid();

            this.Sale = sale;

            addItemsToCollection();
        }

        private void addItemsToCollection()
        {
            List<Item> items = new List<Item>();

            Item A = new Item { ID = Guid.NewGuid(), Name = "A", SKU = "A", Price = 50, Unit = Unit.Kg };
            Item B = new Item { ID = Guid.NewGuid(), Name = "B", SKU = "B", Price = 30, Unit = Unit.Pcs };
            Item C = new Item { ID = Guid.NewGuid(), Name = "B", SKU = "C", Price = 60, Unit = Unit.Pcs };
            Item D = new Item { ID = Guid.NewGuid(), Name = "D", SKU = "D", Price = 15, Unit = Unit.Pcs };

            A.Offers = new List<Offer>();
            B.Offers = new List<Offer>();
            A.Offers.Add(new Offer { ID = Guid.NewGuid(), Item = A, FromDate = DateTime.Now.AddDays(-1), ToDate = DateTime.Now.AddDays(7), Price = 120, Quantity = 3, Unit = Unit.Kg });
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

            ModelCollection.addItems(items);
        }
        private Checkout getCheckout()
        {
            Checkout checkout = new Checkout();
            checkout.ID = Guid.NewGuid();
            checkout.Sale = this.Sale;

            return checkout;
        }
        
        [TestMethod]
        public void add_item_to_collection_test_method()
        {
            List<Item> items = new List<Item>();

            Item A = new Item { ID = Guid.NewGuid(), Name = "A", SKU = "A", Price = 50, Unit = Unit.Kg };

            A.Offers = new List<Offer>();
            A.Offers.Add(new Offer { ID = Guid.NewGuid(), Item = A, FromDate = DateTime.Now.AddDays(-1), ToDate = DateTime.Now.AddDays(7), Price = 120, Quantity = 3, Unit = Unit.Kg });

            ModelCollection.addItem(A);

            var addedItem = ModelCollection.getItems().Where(x => x.ID == A.ID).FirstOrDefault();

            Assert.IsNotNull(addedItem);
            Assert.AreEqual(addedItem.ID, A.ID);
        }

        [TestMethod]
        public void add_items_to_collection_test_method()
        {
            List<Item> items = new List<Item>();

            Item A = new Item { ID = Guid.NewGuid(), Name = "A", SKU = "A", Price = 50, Unit = Unit.Kg };
            Item B = new Item { ID = Guid.NewGuid(), Name = "B", SKU = "B", Price = 30, Unit = Unit.Pcs };
            Item C = new Item { ID = Guid.NewGuid(), Name = "B", SKU = "C", Price = 60, Unit = Unit.Pcs };
            Item D = new Item { ID = Guid.NewGuid(), Name = "D", SKU = "D", Price = 15, Unit = Unit.Pcs };

            A.Offers = new List<Offer>();
            B.Offers = new List<Offer>();
            A.Offers.Add(new Offer { ID = Guid.NewGuid(), Item = A, FromDate = DateTime.Now.AddDays(-1), ToDate = DateTime.Now.AddDays(7), Price = 120, Quantity = 3, Unit = Unit.Kg });
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

            ModelCollection.addItems(items);

            var addedItems = ModelCollection.getItems().Where(x => items.Contains(x));
            foreach (var item in addedItems)
            {
                Item i = items.Where(x => x.ID == item.ID).FirstOrDefault();
                Assert.IsNotNull(item);
                Assert.AreEqual(item.ID, i.ID);
            }
        }

        [TestMethod]
        public void sell_items_in_checkout_test_method()
        {
            Checkout chk = getCheckout();
            bool success = chk.sell(ModelCollection.getItems());

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void add_checkout_to_sale_test_method()
        {
            Checkout chk = getCheckout();
            Sale.addCheckout(chk);

            Assert.IsNotNull(chk);
            Assert.AreEqual(Sale.getCheckouts().LastOrDefault().ID, chk.ID);
        }

        [TestMethod]
        public void get_checkout_total_test_method()
        {
            Checkout chk = getCheckout();

            bool success = chk.sell(ModelCollection.getItems());

            Assert.AreEqual(chk.getTotal(), 350);
        }

        [TestMethod]
        public void get_string_representation_of_checkout_test_method()
        {
            Checkout chk = getCheckout();

            bool success = chk.sell(ModelCollection.getItems());

            string stringRepresentation = chk.getSoldItemsStringRepresentation();

            Assert.AreEqual(string.IsNullOrEmpty(stringRepresentation), string.IsNullOrEmpty(stringRepresentation));
        }
    }
}
