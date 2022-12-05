/*
 * File         : ContractMarketplace.cs
 * Project      : Milestone 4
 * Programmer   : Alex Silveira, Emanuel Juracic, Josh Moore
 * First Version:
 * Description  : This class enables a connection to the Contract Marketplace MySQL database
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
    // Purpose  : The purpose of this class is to establish a connection, utilize the SELECT string statement and bring over the rows contained in a datatable.
    internal class ContractMarketplace
    {


        /*
         * Method      : SetUpConnection
         * Description : This method sets the connection to the database and fills in the contents of the datatable all the rows within the database.
         *               Returns the datatable that will connect to the datatable within the xaml.cs to help output the rows
         * Parameters  : DataTable dt, string connect, string statement   
         * Returns     : DataTable dt
        */
        public DataTable SetUpConnection(DataTable dt, string connect, string statement)
        {
            //try catch blocks utilized for error handling
            try
            {
                //establishing a connecion with the string sent to this method containing the database connect path
                using (MySqlConnection conn = new MySqlConnection(connect))
                {
                    conn.Open();    //open up the connection
                    using (MySqlDataAdapter da = new MySqlDataAdapter(statement, conn)) //using the MySqlDataAdapter with the select statement associated with the connection
                        da.Fill(dt);    //fill up the data table
                    conn.Close();   //close the connect
                }
            }
            //any issues will generate an exception
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
    }
}
