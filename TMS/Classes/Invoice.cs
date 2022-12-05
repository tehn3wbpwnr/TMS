/*
 * File         : Invoice.cs
 * Project      : Milestone 4
 * Programmer   : Alex Silveira, Emanuel Juracic, Josh Moore
 * First Version:
 * Description  : This file contains class related information for the invoice class
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Classes;

namespace TMS
{
    // Name     : Invoice
    // Purpose  : The purpose of this class to hold invoice related data members and methods, such as text file generation of invoices
    internal class Invoice
    {
        public int OrderID { get; set; }
        public decimal CarrierTotal { get; set; }
        public decimal MarkUpTotal  { get; set; }
        public decimal SalesTaxTotal { get; set; }
        public decimal FinalTotal { get; set; }
        public string Date { get; set; }


        /*
         * Method      :  Invoice(int orderID, decimal carrierTotal, decimal markUpTotal, decimal salesTaxTotal, decimal finalTotal, string date)
         * Description : This is a constructor for the invoice class it is used in the creation of an invoice in memory to set all data members related to the invoice upon creation
         * Parameters  : int orderID, decimal carrierTotal, decimal markUpTotal, decimal salesTaxTotal, decimal finalTotal, string date 
         * Returns     : N/A
        */
        public Invoice(int orderID, decimal carrierTotal, decimal markUpTotal, decimal salesTaxTotal, decimal finalTotal, string date)
        {
            OrderID = orderID;
            CarrierTotal = carrierTotal;
            MarkUpTotal = markUpTotal;
            SalesTaxTotal = salesTaxTotal;
            FinalTotal = finalTotal;
            Date = date;
        }


        /*
         * Method      : void GenerateInvoiceText()
         * Description : This is a method that is called to generate the invoice in a text file as stated in the requirements.
         * Parameters  : N/A
         * Returns     : N/A
        */
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
