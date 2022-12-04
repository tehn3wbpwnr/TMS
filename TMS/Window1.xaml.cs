using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
    public partial class TmsDataWindow : Window
    {
        DataTable dt = new DataTable();
        public TmsDataWindow()
        {
            InitializeComponent();
        }

        private void btnCarrierData_Click(object sender, RoutedEventArgs e)
        {
            loadCarrierData();
        }

        private void btnRateFee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRouteTable_Click(object sender, RoutedEventArgs e)
        {
            dt.Columns.Add("City");
            dt.Columns.Add("Distance");
            dt.Columns.Add("Time");
            dt.Columns.Add("West City");
            dt.Columns.Add("East City");

            foreach (City city in RouteTable.corridor)
            {
                dt.Rows.Add(city.city, city.distance.ToString(), city.time.ToString(), city.westCity, city.eastCity);          
            }
            dataShow.ItemsSource = dt.DefaultView;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddWindow aw = new AddWindow();
            aw.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            TmsDatabase td = new TmsDatabase();
            DataRowView row = dataShow.SelectedItems[0] as DataRowView;
            if (row != null)
            {
                td.Connection();
                td.AdminCarrierDataDelete(int.Parse(row.Row.ItemArray[0].ToString()));
                loadCarrierData();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = dataShow.SelectedItems[0] as DataRowView;
            UpdateWindow uw = new UpdateWindow(row);
            uw.Show();
        }

        private void loadCarrierData()
        {
            DataTable dt = new DataTable();
            TmsDatabase tms = new TmsDatabase();
            tms.Connection();
            dt = tms.AdminSelectCarrier(dt);
            dataShow.ItemsSource = dt.DefaultView;
        }
    }
}
