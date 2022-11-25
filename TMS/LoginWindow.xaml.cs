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
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text == "Admin")
            {
                if (txtPassword.Password == "AdminPass")
                {
                    AdminWindow win3 = new AdminWindow();
                    win3.Show();
                    Close();
                }
            }

            else if (txtUserName.Text == "Buyer")
            {
                if (txtPassword.Password == "BuyerPass")
                {
                    BuyerWindow win1 = new BuyerWindow();
                    win1.Show();
                    Close();
                }
            }

            else if (txtUserName.Text == "Planner")
            {
                if (txtPassword.Password == "PlannerPass")
                {
                    PlannerWindow win2 = new PlannerWindow();
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
