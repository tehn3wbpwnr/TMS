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
        public TmsDataWindow()
        {
            InitializeComponent();
        }

        private void btnCarrierData_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            loadCarrierData();

            dataCarrier.Visibility = Visibility.Visible;
            dataRoutes.Visibility = Visibility.Hidden;
            dataRate.Visibility = Visibility.Hidden;

            gridButtons.Visibility = Visibility.Visible;
            gridRateFee.Visibility = Visibility.Hidden;
        }

        private void btnRateFee_Click(object sender, RoutedEventArgs e)
        {
            loadRates();

            dataCarrier.Visibility = Visibility.Hidden;
            dataRoutes.Visibility = Visibility.Hidden;
            dataRate.Visibility = Visibility.Visible;

            gridButtons.Visibility = Visibility.Hidden;
            gridRateFee.Visibility = Visibility.Visible;
        }

        private void btnRouteTable_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("City");
            dt.Columns.Add("Distance");
            dt.Columns.Add("Time");
            dt.Columns.Add("West City");
            dt.Columns.Add("East City");

            foreach (City city in RouteTable.corridor)
            {
                dt.Rows.Add(city.city, city.distance.ToString(), city.time.ToString(), city.westCity, city.eastCity);
            }
            dataRoutes.ItemsSource = dt.DefaultView;

            dataCarrier.Visibility = Visibility.Hidden;
            dataRoutes.Visibility = Visibility.Visible;
            dataRate.Visibility = Visibility.Hidden;

            gridButtons.Visibility = Visibility.Hidden;
            gridRateFee.Visibility = Visibility.Hidden;
        }


        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            bool newValueSet = false;

            try
            {
                if (txtFTLRate.Text != "")
                {
                    RateFee.FTLRates = decimal.Parse(txtFTLRate.Text);
                    newValueSet = true;
                }
                if (txtLTLRate.Text != "")
                {
                    RateFee.LTLRates = decimal.Parse(txtLTLRate.Text);
                    newValueSet = true;
                }
                if (txtFTLMarkup.Text != "")
                {
                    RateFee.FTLMarkUp = decimal.Parse(txtFTLMarkup.Text);
                    newValueSet = true;
                }
                if (txtLTLMarkup.Text != "")
                {
                    RateFee.LTLMarkUp = decimal.Parse(txtLTLMarkup.Text);
                    newValueSet = true;
                }


                if (newValueSet == true)
                {
                    MessageBox.Show("Values have been updated");
                    loadRates();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadCarrierData()
        {
            DataTable dt = new DataTable();
            TmsDatabase tms = new TmsDatabase();
            tms.Connection();
            dt = tms.AdminSelectCarrier(dt);
            dataCarrier.ItemsSource = dt.DefaultView;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddWindow aw = new AddWindow();
            aw.ShowDialog();
            loadCarrierData();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            TmsDatabase td = new TmsDatabase();
            DataRowView row = dataCarrier.SelectedItems[0] as DataRowView;
            if (row != null)
            {
                td.Connection();
                td.AdminCarrierDataDelete(int.Parse(row.Row.ItemArray[0].ToString()));
                loadCarrierData();
            }
        }

        private void loadRates()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FTL Avg Rate");
            dt.Columns.Add("LTL Avg Rate");
            dt.Columns.Add("FTL Markup Rate");
            dt.Columns.Add("LTL Markup Rate");
            dt.Rows.Add(RateFee.FTLRates, RateFee.LTLRates, RateFee.FTLMarkUp, RateFee.LTLMarkUp);
            dataRate.ItemsSource = dt.DefaultView;
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = dataCarrier.SelectedItems[0] as DataRowView;
            UpdateWindow uw = new UpdateWindow(row);
            uw.Show();
        }
    }
}
