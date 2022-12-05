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
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class BuyerWindow : Window
    {
        public BuyerWindow()
        {
            InitializeComponent();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            DataTable loadedContracts = new DataTable();
            dataShow.Visibility = Visibility.Visible;
            ContractMarketplace contractMarketplace = new ContractMarketplace();
            string connect = "Server=159.89.117.198;Database=cmp;Uid=DevOSHT;Pwd= Snodgr4ss!;";
            string statement = "SELECT * FROM Contract;";

            loadedContracts = contractMarketplace.SetUpConnection(loadedContracts, connect, statement);

            dataShow.ItemsSource = loadedContracts.DefaultView;
            dataCompletedOrders.Visibility = Visibility.Hidden;
        }

        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            TmsDatabase tmsDB = new TmsDatabase();
            DataRowView row = dataShow.SelectedItems[0] as DataRowView;
            Order newOrder = new Order(row.Row.ItemArray[0].ToString(),
                                       int.Parse(row.Row.ItemArray[1].ToString()),
                                       int.Parse(row.Row.ItemArray[2].ToString()),
                                       row.Row.ItemArray[3].ToString(),
                                       row.Row.ItemArray[4].ToString(),
                                       int.Parse(row.Row.ItemArray[5].ToString()));
            tmsDB.Connection();
            tmsDB.InsertNewOrder(newOrder.ClientName, newOrder.JobType, newOrder.Quantity, newOrder.Origin, newOrder.Destination, newOrder.TruckType);
        }

        private void btnCompletedOrders_Click(object sender, RoutedEventArgs e)
        {
            DataTable completedContracts = new DataTable();
            dataCompletedOrders.Visibility = Visibility.Visible;
            TmsDatabase tmsDB = new TmsDatabase();
            tmsDB.Connection();
            completedContracts = tmsDB.BuyerSelectCompletedOrders(completedContracts);
            dataCompletedOrders.ItemsSource = completedContracts.DefaultView;
            dataShow.Visibility = Visibility.Hidden;

        }
    }
}
