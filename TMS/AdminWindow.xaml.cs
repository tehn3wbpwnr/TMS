using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
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

        private void LoadLogFile ()
        {
            List<LogFile> log = new List<LogFile>();
            // add the files date, time, contents
            log.Add(new LogFile() { Date = "11/25/2022", Time = "16:08", Contents = "Test3" });
            log.Add(new LogFile() { Date = "11/25/2022", Time = "16:08", Contents = "Test2" });
            log.Add(new LogFile() { Date = "11/24/2022", Time = "16:08", Contents = "Test1" });

            logFiles.ItemsSource = log;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
        }

        public class LogFile
        {
            public string Date { get; set; }
            public string Time { get; set; }
            public string Contents { get; set; }
        }
    }
}
