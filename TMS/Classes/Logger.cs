/*
 * File         : Logger.cs
 * Project      : Milestone 4
 * Programmer   : Alex Silveira, Emanuel Juracic, Josh Moore
 * First Version:
 * Description  : This file contains class related information for the Logger class
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Classes
{
    // Name     : Logger
    // Purpose  : The purpose of this class is to easily enable a logging functionality, it writes to a specified log file from the config file.
    internal class Logger
    {
        string fileName;

        string FileName { get; set; }

        /*
         * Method      :  Logger()
         * Description : This is a constructor for the Logger class, it gets the specified log file path from the config.
         * Parameters  : N/A 
         * Returns     : N/A
        */
        public Logger()
        {
            FileName = ConfigurationManager.AppSettings["LogFilePath"];          
        }

        public void WriteLog(string msg)
        {
            string log = DateTime.Now.ToString();
            log += ": " + msg;

            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(FileName, true);
                writer.WriteLine(log);
                writer.Close();
            }
            catch (Exception e)
            {
                //handle possible exception
            }
            finally
            {
                if(writer != null)
                {
                    writer.Close();
                }
            }
        }

        /*
         * Method      : List<string> LoadLogs()
         * Description : This is a method that is used for the admin window to load a list of our current logs in our log fiels for the logs to be accesible in the window.
         * Parameters  : N/A 
         * Returns     : List<string>: a List of strings that are the lines of the log file.
        */

        public List<string> LoadLogs()
        {
            List<string> logs = new List<string>();
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(FileName);
                string line;
                while(!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    logs.Add(line);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                //handle possible exception
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return logs;
        }


    }
}
