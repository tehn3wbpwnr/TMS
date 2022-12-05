﻿  using System;
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
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class BuyerWindow : Window
    {
        TmsDatabase tmsDB =  new TmsDatabase();
        Logger log = new Logger();
        public BuyerWindow()
        {
            InitializeComponent();
            btnCreateOrder.IsEnabled = false;
            btnCreateInvoice.IsEnabled = false;
        }




        protected override void OnClosing(CancelEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
        }



        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            DataTable loadedContracts = new DataTable();
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




        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = dataContractMarket.SelectedItems[0] as DataRowView;
            Order newOrder = new Order(row.Row.ItemArray[0].ToString(),
                                       int.Parse(row.Row.ItemArray[1].ToString()),
                                       int.Parse(row.Row.ItemArray[2].ToString()),
                                       row.Row.ItemArray[3].ToString(),
                                       row.Row.ItemArray[4].ToString(),
                                       int.Parse(row.Row.ItemArray[5].ToString()));
            tmsDB.Connection();

            tmsDB.InsertNewOrder(newOrder.ClientName, newOrder.JobType, newOrder.Quantity, newOrder.Origin, newOrder.Destination, newOrder.TruckType);
            log.WriteLog("Order was created for " + newOrder.ClientName);

            btnCreateOrder.IsEnabled = false;
        }



        private void btnCompletedOrders_Click(object sender, RoutedEventArgs e)
        {
            DataTable completedContracts = new DataTable();
            dataCompletedOrders.Visibility = Visibility.Visible;
            tmsDB.Connection();
            completedContracts = tmsDB.BuyerSelectCompletedOrders(completedContracts);
            dataCompletedOrders.ItemsSource = completedContracts.DefaultView;
            dataContractMarket.Visibility = Visibility.Hidden;

            btnCreateOrder.IsEnabled = false;
            btnCreateInvoice.IsEnabled = false;
        }


        private void dataContractMarket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnCreateOrder.IsEnabled = true;
            btnCreateInvoice.IsEnabled = false;
        }



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

            btnCreateInvoice.IsEnabled = false;
            btnCreateOrder.IsEnabled = false;
        }

        private void dataCompletedOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnCreateInvoice.IsEnabled = true;
        }
    }
}
