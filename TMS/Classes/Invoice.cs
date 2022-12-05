using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Classes;

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
        
        public void GenerateInvoiceText()
        {
            string fileName = "Order-" + OrderID + " Invoice.txt";
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(fileName);
                writer.WriteLine("Invoice for Order: " +OrderID + " on " + Date);
                writer.WriteLine("Carrier charge: $" + Math.Round(CarrierTotal,2));
                writer.WriteLine("TMS Mark Up: $" + Math.Round(MarkUpTotal,2));
                writer.WriteLine("Sales Tax: $" + Math.Round(SalesTaxTotal,2));
                writer.WriteLine("Total Cost: $" + Math.Round(FinalTotal,2));
                writer.Close();
            }
            catch (Exception e)
            {
                //handle possible exception
                Logger log = new Logger();
                log.WriteLog(e.ToString());
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
    }
}
