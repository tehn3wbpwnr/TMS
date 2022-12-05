/*
 * File         : TmsDatabase.cs
 * Project      : Milestone 4
 * Programmer   : Alex Silveira, Emanuel Juracic, Josh Moore
 * First Version:
 * Description  : This class involves establishing a connection and utilizing various queries related the local TMS Database
 */

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
    // Name     : ContractMarketplace
    // Purpose  : The purpose of this class is to be involved with the local TMS database. This includes various roles utilizing various queries to move, adjust and calculate rows within the tables
    internal class TmsDatabase
    {
        //important MySQL datamembers that will be utilized across different methods
        MySqlConnection conn = null;
        MySqlCommand cmd = null; 
        MySqlDataReader rdr = null;
        MySqlDataAdapter adpt = null;


        /*
         * Method      : Connection
         * Description : This method establishing a connection to the local TMS database
         * Parameters  : None
         * Returns     : None
        */
        public void Connection()
        {
            //try catch blocks used for error checking and validation
            try
            {
                string connect = "Server=127.0.0.1;Database=tms_database;Uid=SETUser;Pwd= Conestoga1;"; //string containing the connection path
                conn = new MySqlConnection(connect);    //establish the connection
            }
            //any issues will generate a message to the user
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        /*
         * Method      : GetNewOrders
         * Description : This method utilizes a SELECT query to bring up contents within the New_Orders table
         * Parameters  : DataTable dt
         * Returns     : DataTable dt
        */
        public DataTable GetNewOrders(DataTable dt)
        {
            string sqlStatem = "SELECT newOrderID, clientName, jobType, quantity, origin, destination, vanType FROM New_Orders";    //string containing the SELECT query

            //try catch blocks for appropriate error handling
            try
            {
                conn.Open();    //open up the connection
                using (MySqlDataAdapter da = new MySqlDataAdapter(sqlStatem, conn)) //using the MySqlDataAdapter with the select statement associated with the connection
                    da.Fill(dt);  //fill up the data table
                conn.Close(); //close the connection
            }
            //any issues will generate a message to the user
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }



        /*
         * Method      : GetCarriers
         * Description : This method utilizes a SELECT query to bring up contents within the Carrier Table. But it will pick up specifically carriers related to the originCity string that is being
         *               passed through this method
         * Parameters  : string originCity
         * Returns     : DataTable dt
        */
        public DataTable GetCarriers(string originCity)
        {
            DataTable dt = new DataTable(); //instantiate a new DataTable
            string sqlStatem = "SELECT * from Carriers WHERE dcity='" + originCity + "'";   //develop a query string that selects a carrier where the city matches the originCity string

            //try catch block for additional error checking and validation
            try
            {
                conn.Open();    //open a connection
                using (MySqlDataAdapter da = new MySqlDataAdapter(sqlStatem, conn)) //using the MySqlDataAdapter with the select statement associated with the connection
                    da.Fill(dt);    //fill up the data table
                conn.Close();   //close the connection
            }
            //any issues will generate a message to the user
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }


        /*
         * Method      : InsertNewOrder
         * Description : This method utilizes an INSERT method that extracts a bunch of strings and input them into the New_Orders table. The values being brough are values related to the Contract Marketplace
         * Parameters  : string client, int jobType, int quantity, string origin, string destination, int vanType
         * Returns     : None
        */
        public void InsertNewOrder(string client, int jobType, int quantity, string origin, string destination, int vanType)
        {
            string sql = "INSERT INTO New_Orders (clientName, jobType, quantity, origin, destination, vanType) VALUES (@client, @jobType, @quantity, @origin, @destination, @vanType);";    //string containing the update query related to the New_Orders Table

            //try catch block for error validation and error checking
            try
            {
                conn.Open();    //open the connection
                using (var command = new MySqlCommand(sql, conn)) //using the MySqlDataAdapter with the select statement associated with the connection
                {
                    //to utilize variables within MySQL queries, the following code converts the "@string" to the associated variable stemming from the parameters
                    //this ensures that the parameter values related to the columns will be written to the table
                    command.Parameters.Add("@client", MySqlDbType.VarChar).Value = client;
                    command.Parameters.Add("@jobtype", MySqlDbType.VarChar).Value = jobType;
                    command.Parameters.Add("@quantity", MySqlDbType.VarChar).Value = quantity;
                    command.Parameters.Add("@origin", MySqlDbType.VarChar).Value = origin;
                    command.Parameters.Add("@destination", MySqlDbType.VarChar).Value = destination;
                    command.Parameters.Add("@vanType", MySqlDbType.VarChar).Value = vanType;
                    command.ExecuteNonQuery();  //execute the query
                }
                conn.Close();   //close the connection
            }
            //display appropriate error message to user in case of error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /*
         * Method      : InsertProcessOrder
         * Description : This method utilizes an INSERT method that extracts a bunch of strings and input them into the process_orders table. The values are related to a row within New_Orders that the planner configures, adds additional columns and pushes it into the process_orders table
         * Parameters  : string client, int jobType, int quantity, string origin, string destination, int vanType, decimal carrierTotal, int numOfTrips
         * Returns     : None
        */
        public void InsertProcessOrder(int orderid, string client, int jobType, int quantity, string origin, string destination, int vanType, decimal carrierTotal, int numOfTrips)
        {
            string sql = "INSERT INTO process_orders (orderID, clientName, jobType, quantity, origin, destination, vanType,  carrierTotal, numOfTrips) VALUES (@orderID, @client, @jobType, @quantity, @origin, @destination, @vanType, @carrierTotal, @numOfTrips);"; //string containing the INSERT query related to the process_orders Table
            //try catch block for error validation and error checking
            try
            {
                conn.Open();    //open the connection
                using (var command = new MySqlCommand(sql, conn))   //using the MySqlDataAdapter with the select statement associated with the connection
                {
                    //to utilize variables within MySQL queries, the following code converts the "@string" to the associated variable stemming from the parameters
                    //this ensures that the parameter values related to the columns will be written to the table
                    command.Parameters.Add("@orderID", MySqlDbType.VarChar).Value = orderid;
                    command.Parameters.Add("@client", MySqlDbType.VarChar).Value = client;
                    command.Parameters.Add("@jobtype", MySqlDbType.VarChar).Value = jobType;
                    command.Parameters.Add("@quantity", MySqlDbType.VarChar).Value = quantity;
                    command.Parameters.Add("@origin", MySqlDbType.VarChar).Value = origin;
                    command.Parameters.Add("@destination", MySqlDbType.VarChar).Value = destination;
                    command.Parameters.Add("@vanType", MySqlDbType.VarChar).Value = vanType;
                    command.Parameters.Add("@carrierTotal", MySqlDbType.VarChar).Value = carrierTotal;
                    command.Parameters.Add("@numOfTrips", MySqlDbType.VarChar).Value = numOfTrips;
                    command.ExecuteNonQuery();  //execute the query
                }
                conn.Close();   //close connection
            }
            //display appropriate error message to user in case of error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /*
         * Method      : InsertCompletedOrder
         * Description : This method utilizes an INSERT method that extracts a bunch of strings and input them into the Completed_Orders table. The values are related to a row within Process_Orders that the planner configures, adds additional columns and pushes it into the Completed_Orders table
         * Parameters  : int orderid, string client, int jobType, int quantity, string origin, string destination, int vanType, decimal carrierTotal, int numOfTrips
         * Returns     : None
        */
        public void InsertCompletedOrder(int orderid, string client, int jobType, int quantity, string origin, string destination, int vanType, decimal carrierTotal, int numOfTrips)
        {
            string sql = "INSERT INTO completed_orders (orderID, clientName, jobType, quantity, origin, destination, vanType,  carrierTotal, numOfTrips) VALUES (@orderID, @client, @jobType, @quantity, @origin, @destination, @vanType, @carrierTotal, @numOfTrips);";    //string containing the INSERT query related to the Completed_Orders Table

            //try catch block for error validation and error checking
            try
            {
                conn.Open();    //open connection
                using (var command = new MySqlCommand(sql, conn))   //using the MySqlDataAdapter with the select statement associated with the connection
                {
                    //to utilize variables within MySQL queries, the following code converts the "@string" to the associated variable stemming from the parameters
                    //this ensures that the parameter values related to the columns will be written to the table
                    command.Parameters.Add("@orderID", MySqlDbType.VarChar).Value = orderid;
                    command.Parameters.Add("@client", MySqlDbType.VarChar).Value = client;
                    command.Parameters.Add("@jobtype", MySqlDbType.VarChar).Value = jobType;
                    command.Parameters.Add("@quantity", MySqlDbType.VarChar).Value = quantity;
                    command.Parameters.Add("@origin", MySqlDbType.VarChar).Value = origin;
                    command.Parameters.Add("@destination", MySqlDbType.VarChar).Value = destination;
                    command.Parameters.Add("@vanType", MySqlDbType.VarChar).Value = vanType;
                    command.Parameters.Add("@carrierTotal", MySqlDbType.VarChar).Value = carrierTotal;
                    command.Parameters.Add("@numOfTrips", MySqlDbType.VarChar).Value = numOfTrips;
                    command.ExecuteNonQuery();  //execute the query
                }
                conn.Close();   //close connection
            }
            //display appropriate error message to user in case of error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /*
         * Method      : DeleteProcessOrder
         * Description : This method utilizes a DELETE statement to delete a row related to the orderID passed as the parameter
         * Parameters  : string orderID
         * Returns     : None
        */
        public void DeleteProcessOrder(string orderID)
        {

            String sqlStatem = "DELETE FROM process_orders WHERE OrderID=" + orderID;   //string containing the DELETE statement that contains the variable of orderID containing the value of what row is gonna be deleted

            //try catch block for error validation and error checking
            try
            {
                adpt = new MySqlDataAdapter();  //instantiate a new MySqlDataAdapter

                cmd = new MySqlCommand(sqlStatem, conn);    //utilize cmd to contain the sql statement alongside the connection

                conn.Open(); //open the connection
                adpt.DeleteCommand = cmd;   //assign the delete command to the content of cmd (containing the DELETE string and connection
                adpt.DeleteCommand.ExecuteNonQuery();   //execute the statement

                cmd.Dispose();  //dispose of the query + connection
                conn.Close();   //end the connection
            }
            //display appropriate error message to user in case of error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*
         * Method      : DeleteNewOrder
         * Description : This method utilizes a DELETE query. It takes a string thats related to the orderID and the query utilized that string to delete the corresponding row
         * Parameters  : string orderID
         * Returns     : None
        */
        public void DeleteNewOrder(string orderID)
        {

            String sqlStatem = "DELETE FROM new_orders WHERE newOrderID=" + orderID;    //string containing the DELETE query alongside the orderID value of what row is gonna be deleted

            //try catch block for error validation and error checking
            try
            {
                adpt = new MySqlDataAdapter();  //instantiate a new MySqlDataAdapter

                cmd = new MySqlCommand(sqlStatem, conn);    //utilize cmd to contain the sql statement alongside the connection

                conn.Open();    //open the connection
                adpt.DeleteCommand = cmd;   //assign the delete command to the content of cmd (containing the DELETE string and connection
                adpt.DeleteCommand.ExecuteNonQuery();   //execute the statement

                cmd.Dispose();  //dispose of the query + connection
                conn.Close();   //end the connection
            }
            //display appropriate error message to user in case of error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        /*
         * Method      : GetProcessOrders
         * Description : This method utilizes a SELECT query to bring up contents within the Process_Orders Table.
         * Parameters  : DataTable dt
         * Returns     : DataTable dt
        */
        public DataTable GetProcessOrders(DataTable dt)
        {
            string sqlStatem = "SELECT OrderID, clientName, jobType, quantity, origin, destination, vanType, carrierTotal, numOfTrips FROM Process_Orders"; //query containing SELECT string extracting colums/values within the table of Process_Orders 

            //try catch block for error validation and error checking
            try
            {
                conn.Open();    //open connection
                using (MySqlDataAdapter da = new MySqlDataAdapter(sqlStatem, conn)) //using the MySqlDataAdapter with the select statement associated with the connection
                    da.Fill(dt);    //fill up the datatable of dt the contents of da(containing the values of Process_Orders table
                conn.Close();   //close the connection
            }
            //display appropriate error message to user in case of error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        public void updateProcessTrips(int orderID, int numOfTrips)
        {
            string sqlStatem = "UPDATE process_orders SET numOfTrips=" + numOfTrips + " WHERE orderID=" + orderID + ";";
            conn.Open();
            MySqlCommand comm = new MySqlCommand(sqlStatem, conn);
            comm.ExecuteNonQuery();
            conn.Close();
        }

        /*
         * Method      : AdminSelectCarrier
         * Description : This method utilizes a SELECT query to bring up contents within the Carriers Table.
         * Parameters  : DataTable dt
         * Returns     : DataTable dt
        */
        public DataTable AdminSelectCarrier(DataTable dt)
        {
            string sqlStatem = "SELECT carrierID, cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge FROM Carriers";    //string query to select columns/values within the Carriers Table

            //try catch block for error validation and error checking
            try
            {
                conn.Open();    //open the connection
                using (MySqlDataAdapter da = new MySqlDataAdapter(sqlStatem, conn)) //using the MySqlDataAdapter with the select statement associated with the connection
                    da.Fill(dt);     //fill up the datatable of dt the contents of da(containing the values of Carriers table
                conn.Close();   //close the connection
            }
            //display appropriate error message to user in case of error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }


        /*
         * Method      : AdminCarrierDataInsert
         * Description : This method utilizes an INSERT statement to enable the Admin to add a carrier to the local Carrier Database
         * Parameters  : string cName, string dCity, int FTLA, int LTLA, decimal FTLRate, decimal LTLRate, decimal reefCharge
         * Returns     : None
        */
        public void AdminCarrierDataInsert(string cName, string dCity, int FTLA, int LTLA, decimal FTLRate, decimal LTLRate, decimal reefCharge)
        {
            string sql = "INSERT INTO Carriers (cName, dCity, FTLA, LTLA, FTLRate, LTLRate, reefCharge) VALUES (@cName, @dCity, @FTLA, @LTLA, @FTLRate, @LTLRate, @reefCharge);";   //string containing the update query related to the Carriers Table
            //try catch block for error validation and error checking
            try
            {
                conn.Open();    //open the connection
                using (var command = new MySqlCommand(sql, conn)) //using the MySqlDataAdapter with the select statement associated with the connection
                {
                    //to utilize variables within MySQL queries, the following code converts the "@string" to the associated variable stemming from the parameters
                    //this ensures that the parameter values related to the columns will be written to the table
                    command.Parameters.Add("@cName", MySqlDbType.VarChar).Value = cName;
                    command.Parameters.Add("@dCity", MySqlDbType.VarChar).Value = dCity;
                    command.Parameters.Add("@FTLA", MySqlDbType.Int64).Value = FTLA;
                    command.Parameters.Add("@LTLA", MySqlDbType.Int64).Value = LTLA;
                    command.Parameters.Add("@FTLRate", MySqlDbType.Decimal).Value = FTLRate;
                    command.Parameters.Add("@LTLRate", MySqlDbType.Decimal).Value = LTLRate;
                    command.Parameters.Add("@reefCharge", MySqlDbType.Decimal).Value = reefCharge;
                    command.ExecuteNonQuery();  //execute the query
                }
                conn.Close();   //close the connection
            }
            //display appropriate error message to user in case of error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /*
         * Method      : AdminCarrierDataDelete
         * Description : This method utilizes a DELETE statement where the parameter specified which row to delete
         * Parameters  : int carrierID
         * Returns     : None
        */
        public void AdminCarrierDataDelete(int carrierID)
        {
            String sqlStatem = "DELETE FROM Carriers WHERE carrierID=" + carrierID; //query containing the DELETE string alongside the value of carrierID of which row will be deleted

            //try catch block for error validation and error checking
            try
            {
                adpt = new MySqlDataAdapter();   //instantiate a new MySqlDataAdapter

                cmd = new MySqlCommand(sqlStatem, conn);    //utilize cmd to contain the sql statement alongside the connection

                conn.Open();    //open connection
                adpt.DeleteCommand = cmd;   //assign the delete command to the content of cmd (containing the DELETE string and connection
                adpt.DeleteCommand.ExecuteNonQuery();   //execute the query

                cmd.Dispose();  //dispose the cmd command
                conn.Close();   //close the connection
            }
            //display appropriate error message to user in case of error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /*
         * Method      : AdminCarrierDataUpdate
         * Description : This method utilizes an UPDATE string query to update the contents within a selected Carrier table row value
         * Parameters  : int carrierID, string cName, string dCity, int FTLA, int LTLA, decimal FTLRate, decimal LTLRate, decimal reefCharge
         * Returns     : None
        */
        public void AdminCarrierDataUpdate(int carrierID, string cName, string dCity, int FTLA, int LTLA, decimal FTLRate, decimal LTLRate, decimal reefCharge)
        {
            string sql = "UPDATE Carriers SET cName=@cName, dCity=@dCity, FTLA=@FTLA, LTLA=@LTLA, FTLRate=@FTLRate, LTLRate=@LTLRate, reefCharge=@reefCharge WHERE carrierID=@carrierID;";  //query to update values within a Carrier row related to the specified carrierID number

            //try catch block for error validation and error checking
            try
            {
                conn.Open();    //open the connection
                using (var command = new MySqlCommand(sql, conn))   //using the MySqlDataAdapter with the select statement associated with the connection
                {
                    //to utilize variables within MySQL queries, the following code converts the "@string" to the associated variable stemming from the parameters
                    //this ensures that the parameter values related to the columns will be updated to the table
                    command.Parameters.Add("@cName", MySqlDbType.VarChar).Value = cName;
                    command.Parameters.Add("@dCity", MySqlDbType.VarChar).Value = dCity;
                    command.Parameters.Add("@FTLA", MySqlDbType.VarChar).Value = FTLA;
                    command.Parameters.Add("@LTLA", MySqlDbType.VarChar).Value = LTLA;
                    command.Parameters.Add("@FTLRate", MySqlDbType.VarChar).Value = FTLRate;
                    command.Parameters.Add("@LTLRate", MySqlDbType.VarChar).Value = LTLRate;
                    command.Parameters.Add("@reefCharge", MySqlDbType.VarChar).Value = reefCharge;
                    command.Parameters.Add("@carrierID", MySqlDbType.VarChar).Value = carrierID;
                    command.ExecuteNonQuery();  //execute the query
                }
                conn.Close();   //close the connection
            }
            //display appropriate error message to user in case of error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /*
         * Method      : BuyerSelectCompletedOrders
         * Description : This method utilizes a SELECT to enable the buyer to view the contents within the Completed_Orders table
         * Parameters  : int carrierID, string cName, string dCity, int FTLA, int LTLA, decimal FTLRate, decimal LTLRate, decimal reefCharge
         * Returns     : None
        */
        public DataTable BuyerSelectCompletedOrders(DataTable dt)
        {
            string sqlStatem = "SELECT orderID, clientName, jobType, quantity, origin, destination, vanType, carrierTotal, numOfTrips FROM Completed_Orders"; //string query to select columns/values within the Completed_Orders Table

            //try catch block for error validation and error checking
            try
            {
                conn.Open();    //open connection
                using (MySqlDataAdapter da = new MySqlDataAdapter(sqlStatem, conn)) //using the MySqlDataAdapter with the select statement associated with the connection
                    da.Fill(dt);    //fill up the datatable of dt the contents of da(containing the values of Completed_Orders table
                conn.Close();   //close connection
            }
            //display appropriate error message to user in case of error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
    }
}
