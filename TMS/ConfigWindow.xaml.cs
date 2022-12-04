/*
* File          : ConfigWindow.xaml.cs
* Project       : Milestone 4
* Programmers   : Alex Silveira, Emanuel Juracic, Josh Moore
* First Version : N/A
* Description   : This is the code behind for the ConfigWindow 
*/
using Microsoft.Win32;
using System;
using System.CodeDom;
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
using System.Configuration;
using System.Windows.Forms;
using System.Xml;

namespace TMS
{

    /*
     * Class    : ConfigWindow : Window
     * Purpose  : This class is the container for the codebehind of the ConfigWindow.xaml.
     */
    public partial class ConfigWindow : Window
    {
        //constants 
        private const string LOG_KEY = "LogFilePath";
        private const string TMS_IP_KEY = "DB_IPAddress";
        private const string TMS_PORT_KEY = "DB_Port";
        private const string IP_MP_KEY = "MP_IPAddress";

        /*
         * Method       : ConfigWindow
         * Description  : Initializes the ConfigWindow
         * Parameters   : None
         * Returns      : None
         */
        public ConfigWindow()
        {
            InitializeComponent();
        }

        /*
         * Method       : btnChangeIPAndPort_Click
         * Description  : This function allows the user to update the up and the port for the TMS database connection
         * Parameters   : object sender
         *                RoutedEventArgs e
         * Returns      : void
         */
        private void btnChangeIPAndPort_Click(object sender, RoutedEventArgs e)
        {
            if (txtIP.Text != null)
            {
                UpdateConfigKey(TMS_IP_KEY, txtIP.Text);
            }
            if (txtPort.Text != null)
            {
                UpdateConfigKey(TMS_PORT_KEY, txtPort.Text);
            }
        }



        /*
         * Method       : btnLogFilePath_Click
         * Description  : This method allows the user to change the location for the LogFile.txt
         * Parameters   : object sender
         *                RoutedEventArgs e
         * Returns      : void
         */
        private void btnLogFilePath_Click(object sender, RoutedEventArgs e)
        {
            string path;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // still needs to transfer contents of the file 
                path = fbd.SelectedPath;
                UpdateConfigKey(LOG_KEY, path);
            }
        }

        /*
         * Method       : btnChangeMpIP_Click
         * Description  : This method allows the user change the ip of the market place connection 
         * Parameters   : object sender
         *                RoutedEventArgs e
         * Returns      : void
         */
        private void btnChangeMpIP_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMpIP.Text))
            {
                UpdateConfigKey(IP_MP_KEY, txtMpIP.Text);   
            }
        }


        /*
         * Title        :
         * Author       :
         * Date         : 
         * Version      : 
         * Availability :
         */
        private void UpdateConfigKey(string strKey, string newValue)

        {

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + @"..\..\App.config");



            if (!ConfigKeyExists(strKey))

            {

                throw new ArgumentNullException("Key", "<" + strKey + "> not find in the configuration.");

            }

            XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");



            foreach (XmlNode childNode in appSettingsNode)

            {

                if (childNode.Attributes["key"].Value == strKey)

                    childNode.Attributes["value"].Value = newValue;

            }

            xmlDoc.Save(AppDomain.CurrentDomain.BaseDirectory + @"..\..\App.config");

            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            System.Windows.MessageBox.Show("Key Upated Successfullly");

        }

        // also stolen
        private bool ConfigKeyExists(string strKey)

        {

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + @"..\..\App.config");

            XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

            foreach (XmlNode childNode in appSettingsNode)

            {

                if (childNode.Attributes["key"].Value == strKey)

                    return true;
            }

            return false;

        }
    }
}
