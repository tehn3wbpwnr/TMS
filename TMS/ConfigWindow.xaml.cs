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
using System.Xml;

namespace TMS
{
    /// <summary>
    /// Interaction logic for ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        private const string key = "LogFilePath";
        public ConfigWindow()
        {
            InitializeComponent();
        }

        private void btnLogFilePath_Click(object sender, RoutedEventArgs e)
        {
            string path;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files (*.txt)|*.txt";
            if (ofd.ShowDialog() == true)
            {
                path = ofd.FileName;
                UpdateConfigKey(key, path);
            }
        }

        public void UpdateConfigKey(string strKey, string newValue)

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

            MessageBox.Show("Key Upated Successfullly");

        }

        // also stolen
        public bool ConfigKeyExists(string strKey)

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
