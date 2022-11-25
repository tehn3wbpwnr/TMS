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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text == "Admin")
            {
                if (txtPassword.Password == "AdminPass")
                {
                    Window3 win3 = new Window3();
                    win3.Show();
                    Close();
                }
            }

            else if (txtUserName.Text == "Buyer")
            {
                if (txtPassword.Password == "BuyerPass")
                {
                    Window1 win1 = new Window1();
                    win1.Show();
                    Close();
                }
            }

            else if (txtUserName.Text == "Planner")
            {
                if (txtPassword.Password == "PlannerPass")
                {
                    Window2 win2 = new Window2();
                    win2.Show();
                    Close();
                }
            }

            else
            {
                lblError.Content = "Invalid Username or Password";
            }
        }
    }
}
