/*
 * File : LoginWindow.xaml.cs
 * Assignment : Milestone 4 
 * Programmers : Alex Silveira, Emanuel Juracic, Josh Moore
 * First Version: November 10th, 2022
 * Description : This file contains the code behind for the login window
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TMS
{
    //Class     : LoginWindow
    //Purpose   : The purpose of this class is to only allow the 3 users to login
    public partial class LoginWindow : Window
    {
        /*
         * Method       : LoginWindow -- Constructor
         * Purpose      : This method initializes the window 
         * Parameters   : none
         * Returns      : none 
         */
        public LoginWindow()
        {
            InitializeComponent();
        }

        /*
         * Method       : btnLoginClick
         * Purpose      : This method validates the user input for correct login and password
         * Parameters   : object sender
         *                RoutedEventArgs e 
         * Returns      : void 
         */
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //error label instantiated to nothing on click
            lblError.Content = ""; 

            //if admin
            if ((txtUserName.Text == "Admin") && (txtPassword.Password == "AdminPass"))
            {
                AdminWindow aw = new AdminWindow();
                aw.Show();
                Close();
            }

            //if buyer
            else if ((txtUserName.Text == "Buyer") && (txtPassword.Password == "BuyerPass"))
            {
                BuyerWindow bw = new BuyerWindow();
                bw.Show();
                Close();
            }

            //if planner
            else if ((txtUserName.Text == "Planner") && (txtPassword.Password == "PlannerPass"))
            {
                PlannerWindow pw = new PlannerWindow();
                pw.Show();
                Close();
            }

            //invalid 
            else
            {
                lblError.Content = "Invalid Username or Password";
            }
        }
    }
}
