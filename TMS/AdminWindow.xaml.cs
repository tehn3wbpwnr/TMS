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
        List<LogFile> lf = new List<LogFile>();
        Admin admin = new Admin();

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
            lf.Clear();
            LoadLogFile();
        }
        
        // load contents into the logfile list
        private void LoadLogFile()
        {
            // this will be changed to read the file contents and assign values per line

            lf.Add(new LogFile() { Date = "11/25/2022", Time = "16:08", Contents = "Test3" });
            lf.Add(new LogFile() { Date = "11/25/2022", Time = "16:08", Contents = "Test2" });
            lf.Add(new LogFile() { Date = "11/24/2022", Time = "16:08", Contents = "Test1" });

            dglogTable.ItemsSource = lf;
            dglogTable.Items.Refresh();
        }

        private void btnTMS_Data_Click(object sender, RoutedEventArgs e)
        {
            ConfigWindow cw = new ConfigWindow();
            cw.ShowDialog();
        }
    }
}
