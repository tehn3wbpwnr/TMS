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
        List<Order> orders = new List<Order>();
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
            tmsDB.getNewOrders(dt);
            initOrders.ItemsSource = dt.DefaultView;
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

            //finally add order to list in memory while window is open when a button is clicked later this list will be added to DB and deleted from memory.
            orders.Add(selectedOrder);
        }
    }
}
