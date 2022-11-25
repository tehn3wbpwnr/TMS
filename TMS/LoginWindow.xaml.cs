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
            lblError.Content = ""; 

            if ((txtUserName.Text == "Admin") && (txtPassword.Password == "AdminPass"))
            {
                AdminWindow aw = new AdminWindow();
                aw.Show();
                Close();
            }

            else if ((txtUserName.Text == "Buyer") && (txtPassword.Password == "BuyerPass"))
            {
                BuyerWindow bw = new BuyerWindow();
                bw.Show();
                Close();
            }

            else if ((txtUserName.Text == "Planner") && (txtPassword.Password == "PlannerPass"))
            {
                PlannerWindow pw = new PlannerWindow();
                pw.Show();
                Close();
            }

            else
            {
                lblError.Content = "Invalid Username or Password";
            }
        }
    }
}
