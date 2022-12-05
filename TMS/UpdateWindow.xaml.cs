/*
    File            : UpdateWindow.xaml.cs
    Project         : Milestone 4 
    Programmers     : Alex Silveira, Emanuel Juracic, Josh Moore
    First Version   : December 1st, 2022
    Description     : This is the code behind file for the UpdateWindow.xaml
*/
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
    /*
     *  Class   : UpdateWindow
     *  Purpose : The class allows the user to update information about an existing carrier. This Class will take in user input and update anything that has been changed to the existing carrier
     */
    public partial class UpdateWindow : Window
    {
        Logger logger = new Logger();

        /*
         * Method       : UpdateWindow -- Constructor
         * Description  : This initializes the UpdateWindow and autopopulates items
         * Parameters   : DataRowView drv
         * Returns      : None
         */
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

        /*
         * Method       : BtnUpdate_Click
         * Description  : This method allows the user to update the existing carrier information selected. The user will be notified if the update doesnt work 
         * Parameters   : object sender
         *                RoutedEventArgs e 
         * Returns      : void 
         */
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // tmsdatabase class 
            TmsDatabase tmsDB = new TmsDatabase();
            //try to connect
            try
            {
                tmsDB.Connection();
                tmsDB.AdminCarrierDataUpdate(int.Parse(txtCarrierID.Text), txtCarrierName.Text, txtCity.Text, int.Parse(txtFTLA.Text), int.Parse(txtLTLA.Text), decimal.Parse(txtFTLRate.Text), decimal.Parse(txtLTLRate.Text), decimal.Parse(txtReefer.Text));
                logger.WriteLog("Carrier Information Updated");
            }

            //exception ex
            catch (Exception ex)
            {
                MessageBox.Show("Error completing update.");
                logger.WriteLog("Exception :" + ex.Message);
            }

        }


    }
}
