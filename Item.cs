using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomshit
{
    public class Product
    {
        public int productId;
        public byte[] productImageData;
        public string productName;
        public double productPrice;
        public int productStock;
        public string Category;

        public Product(int prodId, byte[] prodImgData, string prodName, double prodPrice, int prodStock, string category)
        {
            productId = prodId;
            productImageData = prodImgData;
            productName = prodName;
            productPrice = prodPrice;
            productStock = prodStock;
            Category = category;
        }

        public string getPrice()
        {
            return "₱" + productPrice;
        }
    }


}
