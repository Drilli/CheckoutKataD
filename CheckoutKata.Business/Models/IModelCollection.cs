using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Business.Models
{
    public interface IModelCollection
    {
        List<Item> getItems();

        Item addItem(Item newItem);

        bool addItems(List<Item> newItems);
    }
}
