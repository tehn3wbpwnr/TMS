/* 
 * File         : AdminWindow.xaml.cs
 * Project      : Milestone 4
 * Programmers  : Alex Silveira, Emanual Juracic, Josh Moore
 * First Version: 12/5/2022
 * Description  : This is the codebehind file for AdminWindow.xaml
 */
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math.EC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
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
     *  Class   : AdminWindow
     *  Purpose : The purpose of this class is to display the basic functionality to the admin. The admin can look at the TMS data, Review the logs, config connection settings and create a backup on the database. 
     */
    public partial class AdminWindow : Window
    {
        //logger class
        private Logger logger = new Logger();

        private const string TMS_IP_KEY = "DB_IPAddress";
        private string ip = ConfigurationManager.AppSettings[TMS_IP_KEY];
        /*
        * Method       : AdminWindow -- Constructor
        * Description  : This initializes the AdminWindow and loads the log files
        * Parameters   : none 
        * Returns      : void 
        */
        public AdminWindow()
        {
            InitializeComponent();
            LoadLogFile();
        }

        /*
         * Method       : OnClosing
         * Description  : This will open the login window instead of closing the application 
         * Parameters   : CancelEventArgs e
         * Returns      : void 
         */
        protected override void OnClosing(CancelEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
        }



        /*
         * Method       : btnReferesh_Click
         * Description  : This method we reload the log file 
         * Parameters   : object sender
         *                RoutedEventArgs e 
         * Returns      : void 
         */
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadLogFile();
        }



        /*
         * Method       : LoadLogFile
         * Description  : This will take the information from the logger class and load them into a data grid 
         * Parameters   : none 
         * Returns      : void 
         */
        private void LoadLogFile()
        {
            dglogTable.ItemsSource = logger.LoadLogs();
            dglogTable.Items.Refresh();
        }


        /*
         * Method       : btnTMS_Data_Click
         * Description  : This method opens the window for the TMS data  
         * Parameters   : object sender
         *                RoutedEventArgs e 
         * Returns      : void 
         */
        private void BtnTMS_Data_Click(object sender, RoutedEventArgs e)
        {
            TmsDataWindow tdw = new TmsDataWindow();
            tdw.ShowDialog();
        }

        /*
         * Method       : btnSettings_Click(object sender, RoutedEventArgs e 
         * Description  : This method allows the user to open the settings window
         * Parameters   : object sender
         *                RoutedEventArgs e 
         * Returns      : void 
         */
        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            ConfigWindow cw = new ConfigWindow();
            cw.ShowDialog();
        }



        /*
         * Method       : btnBackup_Click
         * Description  : This method allows the user to chose a file name & path and save a backup from mysql
         * Parameters   : object sender
         *                RoutedEventArgs e 
         * Returns      : void 
         */
        private void BtnBackup_Click(object sender, RoutedEventArgs e)
        {
            string path;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "(*.sql)|*.sql";
            if (sfd.ShowDialog() == true)
            {
                path = sfd.FileName;
                Backup(path);
            }
        }



        /*
        * Title        : void Backup()
        * Author       : adriancs2
        * Date         : October 18th, 2022
        * Version      : v2.3.7
        * Availability : https://github.com/MySqlBackupNET/MySqlBackup.Net
        */
        private void Backup(string file) // added a parameter take in for dynamic naming
        {
            try
            {
                string constring = "Server=" + ip + ";Database=tms_database;Uid=SETUser;Pwd= Conestoga1;"; // our database
                                                                                                           //string file = "C:\\backup.sql";
                using (MySqlConnection conn = new MySqlConnection(constring))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ExportToFile(file);
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.WriteLog("Exception :" + e.Message);
                MessageBox.Show("Failed to backup");
            }
        }
    }
}
