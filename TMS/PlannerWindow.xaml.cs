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

        public PlannerWindow()
        {
            InitializeComponent();
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
        }
        private void btnViewCompleted_Click(object sender, RoutedEventArgs e)
        {
            table = 2;
            // connect 
        }

        private void btnOneDay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddTrip_Click(object sender, RoutedEventArgs e)
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

        
    }
}
