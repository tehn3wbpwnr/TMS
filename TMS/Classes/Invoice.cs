using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    internal class Invoice
    {
        public int OrderID { get; set; }
        public decimal CarrierTotal { get; set; }
        public decimal MarkUpTotal  { get; set; }
        public decimal SalesTaxTotal { get; set; }
        public decimal FinalTotal { get; set; }
        public string Date { get; set; }

        public Invoice(int orderID, decimal carrierTotal, decimal markUpTotal, decimal salesTaxTotal, decimal finalTotal, string date)
        {
            OrderID = orderID;
            CarrierTotal = carrierTotal;
            MarkUpTotal = markUpTotal;
            SalesTaxTotal = salesTaxTotal;
            FinalTotal = finalTotal;
            Date = date;
        }   
    }
}
