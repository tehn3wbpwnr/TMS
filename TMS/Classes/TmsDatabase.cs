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
    internal class TmsDatabase
    {
        MySqlConnection conn = null;    //provides a connection to whatever the database is
        MySqlCommand cmd = null; //execute a specific command
        MySqlDataReader rdr = null;
        MySqlDataAdapter adpt = null;

        public void Connection()
        {
            string connect = "Server=127.0.0.1;Database=tms_database;Uid=SETUser;Pwd= Conestoga1;";
            conn = new MySqlConnection(connect);
        }

        public DataTable SelectStatement(DataTable dt)
        {
            string sqlStatem = "SELECT newOrderID, clientName, jobType, quantity, origin, destination, vanType FROM New_Orders";

            try
            {
                conn.Open();
                using (MySqlDataAdapter da = new MySqlDataAdapter(sqlStatem, conn))
                    da.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        public void InsertStatement(string client, int jobType, int quantity, string origin, string destination, int vanType)
        {
            //String sqlStatem = "INSERT INTO New_Orders (clientName, jobType, quantity, origin, destination, vanType) VALUES (" + client + "," + jobType + "," + quantity + "," + origin + "," + destination + "," + vanType + ");";
            string sql = "INSERT INTO New_Orders (clientName, jobType, quantity, origin, destination, vanType) VALUES (@client, @jobType, @quantity, @origin, @destination, @vanType);";
            conn.Open();
            using (var command = new MySqlCommand(sql, conn))
            {
                command.Parameters.Add("@client", MySqlDbType.VarChar).Value = client;
                command.Parameters.Add("@jobtype", MySqlDbType.VarChar).Value = jobType;
                command.Parameters.Add("@quantity", MySqlDbType.VarChar).Value = quantity;
                command.Parameters.Add("@origin", MySqlDbType.VarChar).Value = origin;
                command.Parameters.Add("@destination", MySqlDbType.VarChar).Value = destination;
                command.Parameters.Add("@vanType", MySqlDbType.VarChar).Value = vanType;
                command.ExecuteNonQuery();
            }
            conn.Close();
        }

        //public void DeleteStatement()
        //{

        //    String sqlStatem = "DELETE FROM Actor WHERE Actor_ID=201";
        //    adpt = new MySqlDataAdapter();

        //    cmd = new MySqlCommand(sqlStatem, conn);

        //    conn.Open();
        //    adpt.DeleteCommand = cmd;
        //    adpt.DeleteCommand.ExecuteNonQuery();

        //    cmd.Dispose();
        //    conn.Close();
        //}


        //public void UpdateStatement()
        //{

        //    String sqlStatem = "UPDATE Actor SET First_Name='Oro', Last_Name='Jackson' WHERE Actor_ID=201";
        //    adpt = new MySqlDataAdapter();

        //    cmd = new MySqlCommand(sqlStatem, conn);

        //    conn.Open();
        //    adpt.UpdateCommand = cmd;
        //    adpt.UpdateCommand.ExecuteNonQuery();

        //    cmd.Dispose();
        //    conn.Close();
        //}

        public DataTable AdminSelectCarrier(DataTable dt)
        {
            string sqlStatem = "SELECT carrierID, cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge FROM Carriers";

            try
            {
                conn.Open();
                using (MySqlDataAdapter da = new MySqlDataAdapter(sqlStatem, conn))
                    da.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

    }
}
