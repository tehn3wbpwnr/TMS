using Microsoft.Win32;
using MySql.Data.MySqlClient;
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
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {

        public AdminWindow()
        {
            InitializeComponent();
            LoadLogFile();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
        }


        // this will re-load the log file
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadLogFile();
        }

        // load contents into the logfile list
        private void LoadLogFile()
        {
            Logger logger = new Logger();
            //test add log to read
            logger.WriteLog("test log 1");
            logger.WriteLog("test log 2");
            //end of test

            dglogTable.ItemsSource = logger.LoadLogs();
            dglogTable.Items.Refresh();
        }

        private void btnTMS_Data_Click(object sender, RoutedEventArgs e)
        {
            //load csv into table
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            ConfigWindow cw = new ConfigWindow();
            cw.ShowDialog();
        }



        private void btnBackup_Click(object sender, RoutedEventArgs e)
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
            string constring = "Server=127.0.0.1;Database=tms_database;Uid=SETUser;Pwd= Conestoga1;"; // our database
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
    }
}
