/*
 * File         : AddWindow.xaml.cs
 * Project      : Milestone 4
 * Programmer   : Alex Silveira, Emanuel Juracic, Josh Moore
 * First Version: nov 10 2022
 * Description  : This file contains window related information for the "add" window
 */
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
    // Name     : addWindow
    // Purpose  : The purpose of this class is to hold window related information for the add window
    public partial class AddWindow : Window
    {
        /*
         * Method      : AddWindow()
         * Description : default window constructor
         * Parameters  : N/A
         * Returns     : N/A
        */
        public AddWindow()
        {
            InitializeComponent();
        }

        /*
         * Method      : void btnAdd_Click(object sender, RoutedEventArgs e)
         * Description : This is a method for the add button click event, add in carrier data from the text boxes for the admin functionality
         * Parameters  : object sender, RoutedEventArgs e
         * Returns     : N/A
        */
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            TmsDatabase tmsDB = new TmsDatabase();

            tmsDB.Connection();
            tmsDB.AdminCarrierDataInsert(txtCarrierName.Text, txtCity.Text, int.Parse(txtFTLA.Text), int.Parse(txtLTLA.Text), decimal.Parse(txtFTLRate.Text), decimal.Parse(txtLTLRate.Text), decimal.Parse(txtReefer.Text));

            this.Close();
        }
    }
}
