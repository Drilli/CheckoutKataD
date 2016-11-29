using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Business.Models
{
    public class ModelCollection : IModelCollection
    {
        private static ModelCollection instance;
        private List<Item> Items { get; set; }

        private ModelCollection() { }

        public static ModelCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ModelCollection();
                }
                return instance;
            }
        }        
        

        public List<Item> getItems()
        {
            return this.Items;
        }

        public Item addItem(Item newItem)
        {
            if (this.Items == null)
            {
                Items = new List<Item>();
            }

            Items.Add(newItem);

            return newItem;
        }

        public bool addItems(List<Item> newItems)
        {
            if (this.Items == null)
            {
                Items = new List<Item>();
            }

            Items.AddRange(newItems);

            return true;
        }
    }
}
