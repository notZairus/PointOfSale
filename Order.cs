using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomshit
{
    public class Order
    {
        public Product order;
        public int quantity;

        public Order(Product ordeR, int quantitY)
        {
            order = ordeR;
            quantity = quantitY;
        }

        public double getTotal()
        {
            return order.productPrice * quantity;
        }
    }
}
