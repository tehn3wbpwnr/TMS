using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Classes
{
    internal class Logger
    {
        string fileName;

        string FileName { get; set; }

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
