using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomshit
{
    public class Transaction
    {
        public string ProductName;
        public string UnitPrice;
        public string Quantity;
        public string OrderTotal;
        public string Date;
        public string TotalAmount;
        public string CashierName;

        public Transaction(string pn, string up, string qua, string ot, string date, string ta, string cn)
        {
            this.ProductName = pn;
            this.UnitPrice = up;
            this.Quantity = qua;
            this.OrderTotal = ot;
            this.Date = date;
            this.TotalAmount = ta;
            this.CashierName = cn;

        }
    }
}
