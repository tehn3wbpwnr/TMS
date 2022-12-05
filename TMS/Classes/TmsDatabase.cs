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
            try
            {
                string connect = "Server=127.0.0.1;Database=tms_database;Uid=SETUser;Pwd= Conestoga1;";
                conn = new MySqlConnection(connect);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DataTable getNewOrders(DataTable dt)
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

        public DataTable getCarriers(string originCity)
        {
            DataTable dt = new DataTable();
            string sqlStatem = "SELECT * from Carriers WHERE dcity='" + originCity + "'";
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

        public void InsertNewOrder(string client, int jobType, int quantity, string origin, string destination, int vanType)
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

        public void InsertProcessOrder(string client, int jobType, int quantity, string origin, string destination, int vanType, decimal carrierTotal, int numOfTrips)
        {

            //String sqlStatem = "INSERT INTO New_Orders (clientName, jobType, quantity, origin, destination, vanType) VALUES (" + client + "," + jobType + "," + quantity + "," + origin + "," + destination + "," + vanType + ");";
            string sql = "INSERT INTO process_orders (clientName, jobType, quantity, origin, destination, vanType,  carrierTotal, numOfTrips) VALUES (@client, @jobType, @quantity, @origin, @destination, @vanType, @carrierTotal, @numOfTrips);";
            conn.Open();
            using (var command = new MySqlCommand(sql, conn))
            {
                command.Parameters.Add("@client", MySqlDbType.VarChar).Value = client;
                command.Parameters.Add("@jobtype", MySqlDbType.VarChar).Value = jobType;
                command.Parameters.Add("@quantity", MySqlDbType.VarChar).Value = quantity;
                command.Parameters.Add("@origin", MySqlDbType.VarChar).Value = origin;
                command.Parameters.Add("@destination", MySqlDbType.VarChar).Value = destination;
                command.Parameters.Add("@vanType", MySqlDbType.VarChar).Value = vanType;
                command.Parameters.Add("@carrierTotal", MySqlDbType.VarChar).Value = carrierTotal;
                command.Parameters.Add("@numOfTrips", MySqlDbType.VarChar).Value = numOfTrips;
                command.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void DeleteNewOrder(string orderID)
        {

            String sqlStatem = "DELETE FROM new_orders WHERE newOrderID=" + orderID;
            adpt = new MySqlDataAdapter();

            cmd = new MySqlCommand(sqlStatem, conn);

            conn.Open();
            adpt.DeleteCommand = cmd;
            adpt.DeleteCommand.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }

        public DataTable getProcessOrders(DataTable dt)
        {
            string sqlStatem = "SELECT OrderID, clientName, jobType, quantity, origin, destination, vanType, carrierTotal, numOfTrips FROM Process_Orders";

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

        public void AdminCarrierDataInsert(string cName, string dCity, int FTLA, int LTLA, decimal FTLRate, decimal LTLRate, decimal reefCharge)
        {

            //String sqlStatem = "INSERT INTO New_Orders (clientName, jobType, quantity, origin, destination, vanType) VALUES (" + client + "," + jobType + "," + quantity + "," + origin + "," + destination + "," + vanType + ");";
            string sql = "INSERT INTO Carriers (cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge) VALUES (@cName, @dCity, @FTLA, @LTLA, @FTLRate, @LTLRate, @reefCharge);";
            conn.Open();
            using (var command = new MySqlCommand(sql, conn))
            {
                command.Parameters.Add("@cName", MySqlDbType.VarChar).Value = cName;
                command.Parameters.Add("@dCity", MySqlDbType.VarChar).Value = dCity;
                command.Parameters.Add("@FTLA", MySqlDbType.Int64).Value = FTLA;
                command.Parameters.Add("@LTLA", MySqlDbType.Int64).Value = LTLA;
                command.Parameters.Add("@FTLRate", MySqlDbType.Decimal).Value = FTLRate;
                command.Parameters.Add("@LTLRate", MySqlDbType.Decimal).Value = LTLRate;
                command.Parameters.Add("@reefCharge", MySqlDbType.Decimal).Value = reefCharge;
                command.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void AdminCarrierDataDelete(int carrierID)
        {
            String sqlStatem = "DELETE FROM Carriers WHERE carrierID=" + carrierID;
            adpt = new MySqlDataAdapter();

            cmd = new MySqlCommand(sqlStatem, conn);

            conn.Open();
            adpt.DeleteCommand = cmd;
            adpt.DeleteCommand.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
        }

        public void AdminCarrierDataUpdate(int carrierID, string cName, string dCity, int FTLA, int LTLA, decimal FTLRate, decimal LTLRate, decimal reefCharge)
        {
            string sql = "UPDATE Carriers SET cName=@cName, dCity=@dCity, FTLA=@FTLA, LTLA=@LTLA, FTLRate=@FTLRate, LTLRate=@LTLRate, reefCharge=@reefCharge WHERE carrierID=@carrierID;";
            conn.Open();
            using (var command = new MySqlCommand(sql, conn))
            {
                command.Parameters.Add("@cName", MySqlDbType.VarChar).Value = cName;
                command.Parameters.Add("@dCity", MySqlDbType.VarChar).Value = dCity;
                command.Parameters.Add("@FTLA", MySqlDbType.VarChar).Value = FTLA;
                command.Parameters.Add("@LTLA", MySqlDbType.VarChar).Value = LTLA;
                command.Parameters.Add("@FTLRate", MySqlDbType.VarChar).Value = FTLRate;
                command.Parameters.Add("@LTLRate", MySqlDbType.VarChar).Value = LTLRate;
                command.Parameters.Add("@reefCharge", MySqlDbType.VarChar).Value = reefCharge;
                command.Parameters.Add("@carrierID", MySqlDbType.VarChar).Value = carrierID;
                command.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
}
