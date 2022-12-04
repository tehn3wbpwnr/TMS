using System;
using System.Collections.Generic;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TmsDataWindow : Window
    {
        public TmsDataWindow()
        {
            InitializeComponent();
        }

        private void btnCarrierData_Click(object sender, RoutedEventArgs e)
        {
            DataTable loadCarriers = new DataTable();
            TmsDatabase tms = new TmsDatabase();
            tms.Connection();
            loadCarriers = tms.AdminSelectCarrier(loadCarriers);
            dataShow.ItemsSource = loadCarriers.DefaultView;
        }

        private void btnRateFee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RouteTable_Click(object sender, RoutedEventArgs e)
        {
            DataTable rt = new DataTable();
        }
    }
}
