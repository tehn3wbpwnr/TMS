/* 
 * File         : AdminData.xaml.cs
 * Project      : Milestone 4
 * Programmers  : Alex Silveira, Emanuel Juracic, Josh Moore
 * First Version: December 1st 2022
 * Description  : This is the codebehind file for the AdminData window.
 */

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
    /*
     *  Name    : TmsDataWindow
     *  Purpose : This class allows the admin to display carrier data, rate fee data and the route table data. As well as allows the admin to modify these values. The admin can
     *            add/delete/update carries, and update the rate fee data as well. 
     */
    public partial class TmsDataWindow : Window
    {
        Logger logger = new Logger();

        /*
         * Method       : TmsDataWindow - Constructor 
         * Description  : Initializes the TmsDataWindow
         * Parameters   : None
         * Returns      : None
         */
        public TmsDataWindow()
        {
            InitializeComponent();
        }

        /*
         * Method       : BtnCarrierData_Click
         * Description  : This method displays the carrier data table to the user and disables un-useable buttons 
         * Parameters   : object sender
         *                RoutedEventArgs e 
         * Returns      : void 
         */
        private void BtnCarrierData_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            LoadCarrierData();

            //display correct datagrid
            dataCarrier.Visibility = Visibility.Visible;
            dataRoutes.Visibility = Visibility.Hidden;
            dataRate.Visibility = Visibility.Hidden;

            //enable the relative buttons 
            gridButtons.Visibility = Visibility.Visible;
            gridRateFee.Visibility = Visibility.Hidden;

            //disable buttons 
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        /*
         * Method       : BtnRateFee_Click
         * Description  : This button shows the rate fee table to the user 
         * Parameters   : object sender
         *                RoutedEventArgs e
         * Returns      : void 
         */
        private void BtnRateFee_Click(object sender, RoutedEventArgs e)
        {
            //load table with rates
            LoadRates();

            //display correct grid
            dataCarrier.Visibility = Visibility.Hidden;
            dataRoutes.Visibility = Visibility.Hidden;
            dataRate.Visibility = Visibility.Visible;

            //change buttons
            gridButtons.Visibility = Visibility.Hidden;
            gridRateFee.Visibility = Visibility.Visible;
        }

        /*
         * Method       : BtnRoutedTable_Click
         * Description  : This function builds and displays the routetable to the user 
         * Parameters   : object sender
         *                RoutedEventArgs e
         * Returns      : void 
         */
        private void BtnRouteTable_Click(object sender, RoutedEventArgs e)
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

        /*
         * Method       : BtnChange_Click
         * Description  : This button allows the user to change rates, fees and markups on TMS data
         * Parameters   : object sender
         *                RoutedEventArgs e
         * Returns      : void 
         */
        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            bool newValueSet = false;

            try
            {
                //update rate
                if (txtFTLRate.Text != "")
                {
                    logger.WriteLog("FTL rate updated from :" + RateFee.FTLRates.ToString() + " to " + txtFTLRate.Text);
                    RateFee.FTLRates = decimal.Parse(txtFTLRate.Text);
                    newValueSet = true;
                }
                //update LTL rate
                if (txtLTLRate.Text != "")
                {
                    logger.WriteLog("LTL rate updated from :" + RateFee.LTLRates.ToString() + " to " + txtLTLRate.Text);
                    RateFee.LTLRates = decimal.Parse(txtLTLRate.Text);
                    newValueSet = true;
                }
                //update FTL markup
                if (txtFTLMarkup.Text != "")
                {
                    logger.WriteLog("FTL markup updated from :" + RateFee.FTLMarkUp.ToString() + " to " + txtFTLMarkup.Text);
                    RateFee.FTLMarkUp = decimal.Parse(txtFTLMarkup.Text);
                    newValueSet = true;
                }
                //update LTL markup
                if (txtLTLMarkup.Text != "")
                {
                    logger.WriteLog("LTL markup updated from :" + RateFee.FTLMarkUp.ToString() + " to " + txtLTLMarkup.Text);
                    RateFee.LTLMarkUp = decimal.Parse(txtLTLMarkup.Text);
                    newValueSet = true;
                }
                //show update complete and show change 
                if (newValueSet == true)
                {
                    MessageBox.Show("Values have been updated");
                    LoadRates();
                }
            }
            //if exception log and display
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update values");
                logger.WriteLog(ex.Message);
            }
        }

        /*
         * Method       : LoadCarrierDate()
         * Description  : This method loads the carrier data from the TMS database
         * Parameters   : None
         * Returns      : void
         */
        private void LoadCarrierData()
        {
            DataTable dt = new DataTable();
            TmsDatabase tms = new TmsDatabase();
            tms.Connection();
            dt = tms.AdminSelectCarrier(dt);
            dataCarrier.ItemsSource = dt.DefaultView;
        }

        /*
         * Method       : BtnAdd_Click
         * Description  : This button opens a new window so that the admin may add a new carrier to the list
         * Parameters   : object sender
         *                RoutedEventArgs e
         * Returns      : void
         */
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddWindow aw = new AddWindow();
            aw.ShowDialog();
            LoadCarrierData();    
        }

        /*
         * Method       : BtnDelete_Click
         * Description  : This button allows the user to delete a carrier from the database
         * Parameters   : object sender
         *                RoutedEventArgs e 
         * Returns      : void 
         */
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            //tms database object 
            TmsDatabase td = new TmsDatabase();
            //get selected item
            DataRowView row = dataCarrier.SelectedItems[0] as DataRowView;
            //if item is selected
            if (row != null)
            {
                //connecct
                td.Connection();
                //delete this information based on id
                td.AdminCarrierDataDelete(int.Parse(row.Row.ItemArray[0].ToString()));
                //log
                logger.WriteLog("Carrier Deleted");
                //feedback
                MessageBox.Show("Carrier Deleted");
                //load carrier data
                LoadCarrierData();
            }
        }

        /*
         * Method       : LoadRates
         * Description  : This method loads the relavent rates and markup into the table
         * Parameters   : none 
         * Returns      : void 
         */
        private void LoadRates()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FTL Avg Rate");
            dt.Columns.Add("LTL Avg Rate");
            dt.Columns.Add("FTL Markup Rate");
            dt.Columns.Add("LTL Markup Rate");

            dt.Rows.Add(RateFee.FTLRates, RateFee.LTLRates, RateFee.FTLMarkUp, RateFee.LTLMarkUp);

            dataRate.ItemsSource = dt.DefaultView;

            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
        }

        /*
         * Method       : BtnUpdate_Click
         * Description  : This button opens a new window so the user may update existing information of a carrier
         * Parameters   : object sender
         *                RotuedEVentArgs e 
         * Returns      : void 
         */
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = dataCarrier.SelectedItems[0] as DataRowView;
            UpdateWindow uw = new UpdateWindow(row);
            uw.Show();

            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
        }

        /*
         * Method       : DataCarrier_SelectionChanged
         * Description  : This method enabled buttons if the user has selected a carrier
         * Parameters   : object sender
         *                SelectionChangedEventArgs e
         * Returns      : void 
         */
        private void DataCarrier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelete.IsEnabled = true;
            btnUpdate.IsEnabled = true; 
        }
    }
}
