using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            TmsDatabase tmsDB = new TmsDatabase();

            tmsDB.Connection();
            tmsDB.AdminCarrierDataInsert(txtCarrierName.Text, txtCity.Text, int.Parse(txtFTLA.Text), int.Parse(txtLTLA.Text), decimal.Parse(txtFTLRate.Text), decimal.Parse(txtLTLRate.Text), decimal.Parse(txtReefer.Text));

            this.Close();
        }
    }
}
