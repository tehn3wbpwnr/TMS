using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TMS.Classes
{
    internal class ContractMarketplace
    {
        public DataTable SetUpConnection(DataTable dt, string connect, string statement)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connect))
                {
                    conn.Open();
                    using (MySqlDataAdapter da = new MySqlDataAdapter(statement, conn))
                        da.Fill(dt);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
    }
}
