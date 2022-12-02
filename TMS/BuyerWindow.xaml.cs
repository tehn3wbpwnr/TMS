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
            ContractMarketplace contractMarketplace = new ContractMarketplace();
            DataTable dt = new DataTable();
            string connect = "Server=159.89.117.198;Database=cmp;Uid=DevOSHT;Pwd= Snodgr4ss!;";
            string statement = "SELECT * FROM Contract;";

            dt = contractMarketplace.SetUpConnection(dt, connect, statement);

            dataShow.ItemsSource = dt.DefaultView;
        }

        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dataShow.SelectedCells != null)
            {
                string str = ""; 
                DataRowView row = (DataRowView)dataShow.SelectedItem;
                for (int i = 0; i > 6; i++)
                {
                    str += row[i].ToString();
                } 
                MessageBox.Show(str); 
            }
            else
            {
                MessageBox.Show("please select a contract");
            }
        }
    }
}
