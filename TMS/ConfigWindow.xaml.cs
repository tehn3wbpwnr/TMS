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
using System.IO;
using System.Xml;
using System.Data.SqlClient;
using TMS.Classes;

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

        Logger log = new Logger();

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
            //variables 
            bool writeLog = false;
            string logContent = ""; 

            //if ip address was added
            if (txtIP.Text != null)
            {
                logContent = "TMS IP was changed from: " + ConfigurationManager.AppSettings[TMS_IP_KEY] + " to " + txtIP.Text + "";   
                UpdateConfigKey(TMS_IP_KEY, txtIP.Text);
                writeLog = true;                
            }
            //if port was added
            if (txtPort.Text != null)
            {
                logContent = "TMS Port was changed from: " + ConfigurationManager.AppSettings[TMS_PORT_KEY] + " to " + txtPort.Text + "";
                UpdateConfigKey(TMS_PORT_KEY, txtPort.Text);
                writeLog = true;   
            }
            //if write log bool was set to true
            if (writeLog == true)
            {
                log.WriteLog(logContent);
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
            string newPath;
            
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    //local variables 
                    string oldPath = ConfigurationManager.AppSettings[LOG_KEY]; //contents of the config key
                    string logContent = File.ReadAllText(oldPath);              // contents of the file 

                    //add the new path
                    newPath = fbd.SelectedPath;                                 
                    newPath += "\\LogFile.txt";
                    //update the config setting
                    UpdateConfigKey(LOG_KEY, newPath);
                    //write contents to "new " file 
                    File.WriteAllText(newPath, logContent); 

                    log.WriteLog("LogFile directory changed to :" + newPath);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Exception: " + ex.Message);
                    log.WriteLog(ex.Message);
                }
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
                try
                {
                    UpdateConfigKey(IP_MP_KEY, txtMpIP.Text);
                    log.WriteLog("Market Place IP");
                }
                catch (Exception ex)
                {
                    log.WriteLog(ex.Message);
                    System.Windows.MessageBox.Show("Configuration Update Failed");
                }
                
            }
        }


        /*
         * Title        : UpdateConfigKey
         * Author       : Rahul Kumar Saxena
         * Date         : September 29th, 2012
         * Version      : 1.0
         * Availability : https://www.c-sharpcorner.com/UploadFile/rahul4_saxena/update-app-config-key-value-at-run-time-in-wpf/
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
            System.Windows.MessageBox.Show("Application Requires Restart To Apply Changes");
        }


        /*
         * Title        : UpdateConfigKey
         * Author       : Rahul Kumar Saxena
         * Date         : September 29th, 2012
         * Version      : 1.0
         * Availability : https://www.c-sharpcorner.com/UploadFile/rahul4_saxena/update-app-config-key-value-at-run-time-in-wpf/
         */
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
