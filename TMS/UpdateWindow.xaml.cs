using MySqlX.XDevAPI.Relational;
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
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        public UpdateWindow(DataRowView drv)
        {
            InitializeComponent();

            txtCarrierID.Text = drv.Row.ItemArray[0].ToString();
            txtCarrierName.Text = drv.Row.ItemArray[1].ToString();
            txtCity.Text = drv.Row.ItemArray[2].ToString();
            txtFTLA.Text = drv.Row.ItemArray[3].ToString();
            txtLTLA.Text = drv.Row.ItemArray[4].ToString();
            txtFTLRate.Text = drv.Row.ItemArray[5].ToString();
            txtLTLRate.Text = drv.Row.ItemArray[6].ToString();
            txtReefer.Text = drv.Row.ItemArray[7].ToString();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            TmsDatabase tmsDB = new TmsDatabase();

            tmsDB.Connection();
            tmsDB.AdminCarrierDataUpdate(int.Parse(txtCarrierID.Text), txtCarrierName.Text, txtCity.Text, int.Parse(txtFTLA.Text), int.Parse(txtLTLA.Text), decimal.Parse(txtFTLRate.Text), decimal.Parse(txtLTLRate.Text), decimal.Parse(txtReefer.Text));
        }


    }
}
