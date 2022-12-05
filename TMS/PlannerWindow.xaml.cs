/*
 * File         : PlannerWindow.xaml.cs 
 * Project      : Milestone 4 
 * Programmer   : Alex Silveira, Emanuel Juracic, Josh Moore
 * First Version: November 10th 2022
 * Description  : This file contains the code behind for the plannerWindow.xaml
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
using TMS.Classes;

namespace TMS
{
    /*
     * Class    : PlannerWindow
     * Purpose  : The purpose of this class is allow the user to recieve orders from the buyer, view in process orders, simulate the passage of one day and print a summary of invoices. 
     *            This can be done in 2 weeks or all time. When the planner selects an order, they can select carries and then add a trip to that order. 
     */
    public partial class PlannerWindow : Window
    {
        //0 = no table //1 = initiatetd orders //2 = completed orders//3 = all invoices//4 = ast two weeks invoices
        private static int table = 0;
        //tms data
        TmsDatabase tmsDB = new TmsDatabase();
        //data tables 
        DataTable dt = new DataTable();
        DataTable processTable = new DataTable();
        DataTable allInvoices = new DataTable();
        DataTable twoWeeksInvoices = new DataTable();
        DataTable carrierTable;
        //order class
        Order inprogressOrder;
        //planner class
        Planner planner = new Planner();
        //log class
        Logger log = new Logger();

        /*
         * Method       : PlannerWindow -- Constructor 
         * Purpose      : This initializes default
         * Parameters   : None
         * Returns      : None
         */
        public PlannerWindow()
        {
            InitializeComponent();
            tmsDB.Connection();
            btnOneDay.IsEnabled = false;
        }


        /*
         * Method       : OnClosing
         * Purpose      : This will open the loginWindow when the planner closes the window
         * Parameters   : CancelEventArgs e
         * Returns      : void
         */
        protected override void OnClosing(CancelEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show(); 
        }

        /*
         * Method       : BtnRecOrder_Click
         * Purpose      : This method displays the orders recieved from the buyer
         * Parameters   : object sender
         *                RoutedEventArgs e
         * Returns      : Void
         */
        private void btnRecOrder_Click(object sender, RoutedEventArgs e)
        {
            table = 1;
            // connect method 
            dt.Clear();
            tmsDB.GetNewOrders(dt);
            initOrders.ItemsSource = dt.DefaultView;

            btnCheckCarriers.IsEnabled = true;
            btnRecOrder.IsEnabled = false;
            btnInvoiceAll.IsEnabled = false;
            btnViewProcess.IsEnabled = false;
            btnTwoWeeksInvoice.IsEnabled = false;
        }

        /*
         * Method       : btnViewInProcess_Click
         * Purpose      : This method shows the planner the contracts in process
         * Parameters   : object sender
         *                RoutedEventArgs e 
         * Returns      : void 
         */
        private void btnViewInProcess_Click(object sender, RoutedEventArgs e)
        {
            table = 2;
            processTable.Clear();
            tmsDB.GetProcessOrders(processTable);
            initOrders.ItemsSource = processTable.DefaultView;

            //disable buttons
            btnRecOrder.IsEnabled = false;
            btnOneDay.IsEnabled = true;
        }

        /*
         * Method       : btnOneDay_Click
         * Purpose      : This method simulates the passage of one day for the planner. removing a trip from the orders
         * Parameters   : object sender
         *                RoutedEventArgs e 
         * Returns      : void 
         */
        private void btnOneDay_Click(object sender, RoutedEventArgs e)
        {
            //clear current table
            processTable.Clear();
            //get the process orders
            tmsDB.GetProcessOrders(processTable);
            
            //from the data row 
            foreach (DataRow row in processTable.Rows)
            {
                Order tempOrder = new Order(int.Parse(row.ItemArray[0].ToString()),
                                 row.ItemArray[1].ToString(),
                                 int.Parse(row.ItemArray[2].ToString()),
                                 int.Parse(row.ItemArray[3].ToString()),
                                 row.ItemArray[4].ToString(),
                                 row.ItemArray[5].ToString(),
                                 int.Parse(row.ItemArray[6].ToString()),
                                 decimal.Parse(row.ItemArray[7].ToString()),
                                 int.Parse(row.ItemArray[8].ToString()));

                //if num of trips is not 0
                if (tempOrder.NumOfTrips != 0)
                {
                    //process the order further
                    tempOrder.NumOfTrips = tempOrder.NumOfTrips - 1;
                    //if it is 0 now
                    if (tempOrder.NumOfTrips == 0)
                    {
                        //delete from process order taable and add to complete orders
                        tmsDB.DeleteProcessOrder(tempOrder.OrderId.ToString());
                        tmsDB.InsertCompletedOrder(tempOrder.OrderId,tempOrder.ClientName, tempOrder.JobType, tempOrder.Quantity, tempOrder.Origin, tempOrder.Destination, tempOrder.TruckType, tempOrder.CarrierTotal, tempOrder.NumOfTrips);
                        log.WriteLog("OrderID: " + tempOrder.OrderId + " was successfully completed!");
                    }
                    // update the current order
                    else
                    {
                        tmsDB.updateProcessTrips(tempOrder.OrderId, tempOrder.NumOfTrips);
                    }
                }
            }
            //once done get new table and load
            processTable.Rows.Clear();
            tmsDB.GetProcessOrders(processTable);
            initOrders.ItemsSource = processTable.DefaultView;
            btnRecOrder.IsEnabled = true;
        }

        /*
         * Method       : btnInvoice_Click
         * Purpose      : This method displays the invoices to the user
         * Parameters   : object sender
         *                RoutedEventArgs e 
         * Returns      : void 
         */
        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            allInvoices.Clear();
            allInvoices = tmsDB.GetInvoice(allInvoices);
            initOrders.ItemsSource = allInvoices.DefaultView;
        }

        /*
         * Method       : updateTable
         * Purpose      : This method updates the data table
         * Parameters   : int status : which table are we using
         * Returns      : void 
         */
        private void updateTable(int status)
        {
            DataTable dt = new DataTable();
        }

        /*
         * Method       : btnCheckCarriers_Click
         * Purpose      : This method allows the user to check the carriers available for that order
         * Parameters   : object sender
         *                RoutedEventArgs e 
         * Returns      : void 
         */
        private void btnCheckCarriers_Click(object sender, RoutedEventArgs e)
        {
            //check if an order was selected
            try
            {
                DataRowView row = initOrders.SelectedItems[0] as DataRowView;
                Order selectedOrder = new Order(int.Parse(row.Row.ItemArray[0].ToString()),
                                     row.Row.ItemArray[1].ToString(),
                                     int.Parse(row.Row.ItemArray[2].ToString()),
                                     int.Parse(row.Row.ItemArray[3].ToString()),
                                     row.Row.ItemArray[4].ToString(),
                                     row.Row.ItemArray[5].ToString(),
                                     int.Parse(row.Row.ItemArray[6].ToString()));
                //find carriers with matching origin city
                carrierTable = tmsDB.GetCarriers(selectedOrder.Origin);
                initOrders.ItemsSource = carrierTable.DefaultView;

                //store temporarily
                inprogressOrder = selectedOrder;
                btnCheckCarriers.IsEnabled = false;
                btnAddTrip.IsEnabled = true;
            }
            //if not, display error to the user
            catch(Exception ex)
            {
                MessageBox.Show("Please select a row first!");
            }
        }


        /*
         * Method       : btnAddTrip_Click
         * Purpose      : This method allows the user to add a trip to the order
         * Parameters   : object sender
         *                RoutedEventArgs e 
         * Returns      : void 
         */
        private void btnAddTrip_Click(object sender, RoutedEventArgs e)
        {
            //get selected row
            DataRowView row = null;
            try
            {
                row = initOrders.SelectedItems[0] as DataRowView;
            }
            // if nothing selected, error
            catch
            {
                //notify user
                MessageBox.Show("Please select a carrier first!");
                return;
            }

            //find if the order is eastbound
            bool isEastBound = planner.isEastBound(inprogressOrder);


            int kmTotal = 0;
            double timeTotal = 0;
            
            //if it is east bound
            if (isEastBound)
            {
                //index and find the distance of the order
                for (int index = RouteTable.corridor.FindIndex(a => a.city.Contains(inprogressOrder.Origin)); index < RouteTable.corridor.Count; index++)
                {
                    if (RouteTable.corridor[index].city.Contains(inprogressOrder.Destination))
                    {
                        break;
                    }
                    kmTotal += RouteTable.corridor[index].distance;
                    timeTotal += RouteTable.corridor[index].time;
                }
            }
            else
            {
                //westbound start from end and count down
                for (int index = (RouteTable.corridor.FindIndex(a => a.city.Contains(inprogressOrder.Origin)) - 1); index >= 0; index--)
                {
                    
                    kmTotal += RouteTable.corridor[index].distance;
                    timeTotal += RouteTable.corridor[index].time;

                    //if check after addition due to table setup
                    if (RouteTable.corridor[index].city.Contains(inprogressOrder.Destination))
                    {
                        break;
                    }
                }
            }



            decimal Total;

            //check the job type
            if (inprogressOrder.JobType == 0)//0 is an FTL truck
            {
                //get selected carriers FTL rate
                decimal ftlRate = decimal.Parse(row.Row.ItemArray[5].ToString());
                Total = ftlRate * kmTotal;
                if (inprogressOrder.TruckType == 1)
                {
                    Total = Total * (1 + decimal.Parse(row.Row.ItemArray[7].ToString()));
                }

            }
            else// LTL truck
            {
                decimal ltlRate = decimal.Parse(row.Row.ItemArray[6].ToString());
                Total = ltlRate * kmTotal * inprogressOrder.Quantity;
                if (inprogressOrder.TruckType == 1)
                {
                    Total = Total * (1 + decimal.Parse(row.Row.ItemArray[7].ToString()));
                }
            }
            int numOfTrips = 1;

            //if order cannot be done in one trip
            if(timeTotal > 8)
            {
                numOfTrips++;//doesn't currently count load/unload just drive time
                Total = Total + 150;
            }


            //assign data members discoverd values
            inprogressOrder.CarrierTotal = Total;
            inprogressOrder.NumOfTrips = numOfTrips;
            carrierTable.Clear();
            
            //put this order into the new inprogress order table with the added total and numoftrips as new columns
            tmsDB.InsertProcessOrder(inprogressOrder.OrderId, inprogressOrder.ClientName, inprogressOrder.JobType, inprogressOrder.Quantity, inprogressOrder.Origin, inprogressOrder.Destination, inprogressOrder.TruckType, Total, numOfTrips);

            btnAddTrip.IsEnabled = false;
            btnRecOrder.IsEnabled = true;
            btnInvoiceAll.IsEnabled = true;
            btnTwoWeeksInvoice.IsEnabled = true;
            btnViewProcess.IsEnabled = true;
            //remove from new orders

            //delete inprogress order
            tmsDB.DeleteNewOrder(inprogressOrder.OrderId.ToString());
            log.WriteLog("Trip was successffuly added to order number: " + inprogressOrder.OrderId);
        }

        /*
         * Method       : btnTwoWeekInvoice_Click
         * Purpose      : This method shows the user invoices from the past two weeks
         * Parameters   : object sender
         *                RoutedEventArgs e 
         * Returns      : void 
         */
        private void btnTwoWeeksInvoice_Click(object sender, RoutedEventArgs e)
        {
            //clear current values
            twoWeeksInvoices.Clear();
            //get the invoices up to 2 weeks
            twoWeeksInvoices = tmsDB.PlannerGetTwoWeeksInvoice(twoWeeksInvoices);
            //assign it to the datagrid
            initOrders.ItemsSource = twoWeeksInvoices.DefaultView;
        }
    }
}
