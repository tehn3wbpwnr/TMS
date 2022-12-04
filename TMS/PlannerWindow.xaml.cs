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
        //0 = no table //1 = initiatetd orders //2 = completed orders
        private static int table = 0;
        TmsDatabase tmsDB = new TmsDatabase();
        DataTable dt = new DataTable();
        Order inprogressOrder;
        Planner planner = new Planner();
        DataTable carrierTable;

        public PlannerWindow()
        {
            InitializeComponent();
            tmsDB.Connection();
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
            tmsDB.getNewOrders(dt);
            initOrders.ItemsSource = dt.DefaultView;
            btnCheckCarriers.IsEnabled = true;
            btnRecOrder.IsEnabled = false;
        }
        private void btnViewCompleted_Click(object sender, RoutedEventArgs e)
        {
            table = 2;
            // connect 
        }

        private void btnOneDay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMarkComplete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateTable(int status)
        {
            DataTable dt = new DataTable();
        }

        private void btnCheckCarriers_Click(object sender, RoutedEventArgs e)
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
            carrierTable = tmsDB.getCarriers(selectedOrder.Origin);
            initOrders.ItemsSource = carrierTable.DefaultView;

            //store temporarily
            inprogressOrder = selectedOrder;
            btnCheckCarriers.IsEnabled = false;
            btnAddTrip.IsEnabled = true;
        }

        private void btnAddTrip_Click(object sender, RoutedEventArgs e)
        {
            //get selected row
            DataRowView row = initOrders.SelectedItems[0] as DataRowView;

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

            }
            else// LTL truck
            {
                decimal ltlRate = decimal.Parse(row.Row.ItemArray[6].ToString());
                Total = ltlRate * kmTotal;
            }

            int numOfTrips = 1;

            if(timeTotal > 8)
            {
                numOfTrips++;//doesn't currently count load/unload just drive time
            }


            inprogressOrder.CarrierTotal = Total;
            inprogressOrder.NumOfTrips = numOfTrips;
            carrierTable.Clear();
            
            //put this order into the new inprogress order table with the added total and numoftrips as new columns
            tmsDB.InsertProcessOrder(inprogressOrder.ClientName, inprogressOrder.JobType, inprogressOrder.Quantity, inprogressOrder.Origin, inprogressOrder.Destination, inprogressOrder.TruckType, Total, numOfTrips);

            btnAddTrip.IsEnabled = false;
            btnRecOrder.IsEnabled = true;

            //remove from new orders

            tmsDB.DeleteNewOrder(inprogressOrder.OrderId.ToString());
        }
    }
}
