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
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class PlannerWindow : Window
    {
        //0 = no table //1 = initiatetd orders //2 = completed orders//3 = all invoices//4 = ast two weeks invoices
        private static int table = 0;
        TmsDatabase tmsDB = new TmsDatabase();
        DataTable dt = new DataTable();
        DataTable processTable = new DataTable();
        DataTable allInvoices = new DataTable();
        DataTable twoWeeksInvoices = new DataTable();
        Order inprogressOrder;
        Planner planner = new Planner();
        DataTable carrierTable;
        Logger log = new Logger();

        public PlannerWindow()
        {
            InitializeComponent();
            tmsDB.Connection();
            btnOneDay.IsEnabled = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show(); 
        }
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
        private void btnViewInProcess_Click(object sender, RoutedEventArgs e)
        {
            table = 2;
            processTable.Clear();
            tmsDB.GetProcessOrders(processTable);
            initOrders.ItemsSource = processTable.DefaultView;
            btnRecOrder.IsEnabled = false;
            btnOneDay.IsEnabled = true;
        }

        private void btnOneDay_Click(object sender, RoutedEventArgs e)
        {
            processTable.Clear();
            tmsDB.GetProcessOrders(processTable);

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

                if (tempOrder.NumOfTrips != 0)
                {
                    tempOrder.NumOfTrips = tempOrder.NumOfTrips - 1;
                    if (tempOrder.NumOfTrips == 0)
                    {
                        tmsDB.DeleteProcessOrder(tempOrder.OrderId.ToString());
                        tmsDB.InsertCompletedOrder(tempOrder.OrderId,tempOrder.ClientName, tempOrder.JobType, tempOrder.Quantity, tempOrder.Origin, tempOrder.Destination, tempOrder.TruckType, tempOrder.CarrierTotal, tempOrder.NumOfTrips);
                        log.WriteLog("OrderID: " + tempOrder.OrderId + " was successfully completed!");
                    }
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

        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            allInvoices.Clear();
            allInvoices = tmsDB.GetInvoice(allInvoices);
            initOrders.ItemsSource = allInvoices.DefaultView;
        }

        private void updateTable(int status)
        {
            DataTable dt = new DataTable();
        }

        private void btnCheckCarriers_Click(object sender, RoutedEventArgs e)
        {
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
            catch(Exception ex)
            {
                MessageBox.Show("Please select a row first!");
            }
        }

        private void btnAddTrip_Click(object sender, RoutedEventArgs e)
        {
            //get selected row
            DataRowView row = null;
            try
            {
                row = initOrders.SelectedItems[0] as DataRowView;
            }
            catch
            {
                MessageBox.Show("Please select a carrier first!");
                return;
            }

            bool isEastBound = planner.isEastBound(inprogressOrder);


            int kmTotal = 0;
            double timeTotal = 0;
            
            if (isEastBound)
            {
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

            if(timeTotal > 8)
            {
                numOfTrips++;//doesn't currently count load/unload just drive time
                Total = Total + 150;
            }


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

            tmsDB.DeleteNewOrder(inprogressOrder.OrderId.ToString());
            log.WriteLog("Trip was successffuly added to order number: " + inprogressOrder.OrderId);
        }

        private void btnTwoWeeksInvoice_Click(object sender, RoutedEventArgs e)
        {
            twoWeeksInvoices.Clear();
            twoWeeksInvoices = tmsDB.PlannerGetTwoWeeksInvoice(twoWeeksInvoices);
            initOrders.ItemsSource = twoWeeksInvoices.DefaultView;
        }
    }
}
