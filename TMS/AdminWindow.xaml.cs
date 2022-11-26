using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
        // list of log file classes, just a container of info 

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


            // this will be changed to read the file contents and assign values per line
            List<string> logs = new List<string>();
            logs = logger.LoadLogs();
            dglogTable.ItemsSource = logs;
            dglogTable.Items.Refresh();
        }
    }
}
