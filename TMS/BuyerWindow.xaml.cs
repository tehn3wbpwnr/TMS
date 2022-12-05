/*
 * File         : BuyerWindow.xaml.cs
 * Project      : Milestone 4
 * Programmer   : Alex Silveira, Emanuel Juracic, Josh Moore
 * First Version: nov 10 2022
 * Description  : This file contains window related information for the "add" window
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using TMS.Classes;

namespace TMS
{
    // Name     : buyerwindow
    // Purpose  : The purpose of this class is to hold window related information for the buyer window
    public partial class BuyerWindow : Window
    {
        //properties
        TmsDatabase tmsDB =  new TmsDatabase();

        DataTable loadedContracts = new DataTable();
        Logger log = new Logger();

        DataTable completedContracts = new DataTable();

        //methods

        /*
         * Method      : BuyerWindow()
         * Description : default window constructor, sets some properties aswell
         * Parameters  : N/A
         * Returns     : N/A
        */
        public BuyerWindow()
        {
            InitializeComponent();
            btnCreateOrder.IsEnabled = false;
            btnCreateInvoice.IsEnabled = false;
            tmsDB.Connection();
        }

        /*
         * Method      : OnClosing
         * Description : event method related to closing the window to reopen the login window
         * Parameters  : N/A
         * Returns     : N/A
        */
        protected override void OnClosing(CancelEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
        }


        /*
         * Method      : btnConnect_Click
         * Description : the event method for the button related to connecting to getting the contracts available from the contract marketplace
         * Parameters  : default event args
         * Returns     : N/A
        */
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            loadedContracts.Rows.Clear();
            dataContractMarket.Visibility = Visibility.Visible;
            ContractMarketplace contractMarketplace = new ContractMarketplace();
            string connect = "Server=159.89.117.198;Database=cmp;Uid=DevOSHT;Pwd= Snodgr4ss!;";
            string statement = "SELECT * FROM Contract;";

            loadedContracts = contractMarketplace.SetUpConnection(loadedContracts, connect, statement);
            log.WriteLog("Contracts successfully pulled from Contract MarketPlace");

            dataContractMarket.ItemsSource = loadedContracts.DefaultView;
            dataCompletedOrders.Visibility = Visibility.Hidden;

            btnCreateOrder.IsEnabled = false;
            btnCreateInvoice.IsEnabled = false;
            //btnCompletedOrders.IsEnabled = false;   
        }



        /*
         * Method      : btnCreateOrder_Click
         * Description : the event method for creating an order from the data we pull from the contract marketplace
         * Parameters  : default event args
         * Returns     : N/A
        */
        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = dataContractMarket.SelectedItems[0] as DataRowView;
            Order newOrder = new Order(row.Row.ItemArray[0].ToString(),
                                       int.Parse(row.Row.ItemArray[1].ToString()),
                                       int.Parse(row.Row.ItemArray[2].ToString()),
                                       row.Row.ItemArray[3].ToString(),
                                       row.Row.ItemArray[4].ToString(),
                                       int.Parse(row.Row.ItemArray[5].ToString()));

            tmsDB.InsertNewOrder(newOrder.ClientName, newOrder.JobType, newOrder.Quantity, newOrder.Origin, newOrder.Destination, newOrder.TruckType);
            log.WriteLog("Order was created for " + newOrder.ClientName);


            foreach (DataRow rows in loadedContracts.Rows)
            {
                if (rows["Client_Name"].ToString() == row.Row.ItemArray[0].ToString() && rows["Origin"].ToString() == row.Row.ItemArray[3].ToString() && rows["Destination"].ToString() == row.Row.ItemArray[4].ToString())
                {
                    rows.Delete();
                    break;
                }
            }
            loadedContracts.AcceptChanges();
            btnCreateOrder.IsEnabled = false;
        }


        /*
         * Method      : btnCompletedOrders_Click
         * Description : This method is used as an event for the button press that will laod our completed orders from the DB into a datagrid in the window
         * Parameters  : N/A
         * Returns     : N/A
        */
        private void btnCompletedOrders_Click(object sender, RoutedEventArgs e)
        {

            completedContracts.Rows.Clear();
            dataCompletedOrders.Visibility = Visibility.Visible;
            tmsDB.BuyerSelectCompletedOrders(completedContracts);

            dataCompletedOrders.ItemsSource = completedContracts.DefaultView;
            dataContractMarket.Visibility = Visibility.Hidden;

            btnCreateOrder.IsEnabled = false;
            btnCreateInvoice.IsEnabled = false;
        }

        /*
         * Method      : dataContractMarket_SelectionChanged
         * Description : this method is used as event to control user workflow disabling/enabling buttons until a selection is made
         * Parameters  : default event args
         * Returns     : N/A
        */
        private void dataContractMarket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnCreateOrder.IsEnabled = true;
            btnCreateInvoice.IsEnabled = false;
        }


        /*
         * Method      : btnCreateInvoice_Click
         * Description : event method for invoice create button, pulls info from selected row and handles creating n invoice for that order
         * Parameters  : N/A
         * Returns     : N/A
        */
        private void btnCreateInvoice_Click(object sender, RoutedEventArgs e)
        {
            //load row
            DataRowView row = dataCompletedOrders.SelectedItems[0] as DataRowView;
            //create temp order based off of that row
            Order tempOrder = new Order(int.Parse(row.Row.ItemArray[0].ToString()),
                                 row.Row.ItemArray[1].ToString(),
                                 int.Parse(row.Row.ItemArray[2].ToString()),
                                 int.Parse(row.Row.ItemArray[3].ToString()),
                                 row.Row.ItemArray[4].ToString(),
                                 row.Row.ItemArray[5].ToString(),
                                 int.Parse(row.Row.ItemArray[6].ToString()),
                                 decimal.Parse(row.Row.ItemArray[7].ToString()),
                                 int.Parse(row.Row.ItemArray[8].ToString()));
            //get markup
            decimal markUp;
            if(tempOrder.JobType == 0)
            {
                markUp = RateFee.FTLMarkUp;
            }
            else
            {
                markUp = RateFee.LTLMarkUp;
            }

            //variables for invoice
            decimal salesTax = Math.Round(RateFee.salesTax * ((tempOrder.CarrierTotal * markUp) + tempOrder.CarrierTotal),2);
            markUp = Math.Round(tempOrder.CarrierTotal * markUp,2);
            decimal finalTotal = Math.Round(tempOrder.CarrierTotal + markUp + salesTax,2);
            string date = DateTime.Now.ToString("yyyy/MM/dd");

            //create invoice
            Invoice newInvoice = new Invoice(tempOrder.OrderId, tempOrder.CarrierTotal, markUp, salesTax, finalTotal, date);
            log.WriteLog("Invoice was created with orderID: " + tempOrder.OrderId);
            //push invoice to db

            tmsDB.InsertNewInvoice(newInvoice.OrderID, newInvoice.CarrierTotal, newInvoice.MarkUpTotal, newInvoice.SalesTaxTotal, newInvoice.FinalTotal, newInvoice.Date);

            //write to file
            newInvoice.GenerateInvoiceText();

            //delete order from completed order table
            tmsDB.DeleteCompletedOrder(newInvoice.OrderID.ToString());

            completedContracts.Rows.Clear();
            tmsDB.BuyerSelectCompletedOrders(completedContracts);

            btnCreateInvoice.IsEnabled = false;
            btnCreateOrder.IsEnabled = false;
        }

        /*
         * Method      : dataCompletedOrders_SelectionChanged
         * Description : this method is used as event to control user workflow disabling/enabling buttons until a selection is made
         * Parameters  : default event args
         * Returns     : N/A
        */
        private void dataCompletedOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnCreateInvoice.IsEnabled = true;
        }
    }
}
