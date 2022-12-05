/*
 * File         : RouteTable.cs 
 * Project      : Milestone 4 
 * Programmer   : Alex Silveira, Emanuel Juracic, Josh Moore
 * First Version: November 10th 2022
 * Description  : This file contains the contents of the route table from a .csv file
 */
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Classes
{
    //Class     : RouteTable 
    //Purpose   : This class is a container for the route table 
    static internal class RouteTable
    {
        //public list
        public static List<City> corridor = new List<City>();

        /*
         * Method: RouteTable -- Constructor 
         * Description: This is the constructor that initializes the list with data members
         * Parameters: None
         * Returns: None
         */
        static RouteTable()
        {
            corridor.Add(new City("Windsor", 191, 2.5, "END", "London"));
            corridor.Add(new City("London", 128, 1.75, "Windsor", "Hamilton"));
            corridor.Add(new City("Hamilton", 68, 1.25, "London", "Toronto"));
            corridor.Add(new City("Toronto", 60, 1.3, "Hamilton", "Oshawa"));
            corridor.Add(new City("Oshawa", 134, 1.65, "Toronto", "Belleville"));
            corridor.Add(new City("Belleville", 82, 1.2, "Oshawa", "Kingston"));
            corridor.Add(new City("Kingston", 196, 2.5, "Belleville", "Ottawa"));
            corridor.Add(new City("Ottawa", 0, 0, "Kingston", "END"));
        }



        //https://qawithexperts.com/article/c-/ways-to-convert-list-into-datatable-using-c/92
        //public static DataTable ToDataTable<T>(this IList<T> data)
        //{
        //    PropertyDescriptorCollection properties =
        //        TypeDescriptor.GetProperties(typeof(T));
        //    DataTable table = new DataTable();
        //    foreach (PropertyDescriptor prop in properties)
        //        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        //    foreach (T item in data)
        //    {
        //        DataRow row = table.NewRow();
        //        foreach (PropertyDescriptor prop in properties)
        //            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
        //        table.Rows.Add(row);
        //    }
        //    return table;
        //}

        //public static DataTable ConvertListToDataTable(List<string[]> list)
        //{
        //    // New table.
        //    DataTable table = new DataTable();

        //    // Get max columns.
        //    int columns = 0;
        //    foreach (var array in list)
        //    {
        //        if (array.Length > columns)
        //        {
        //            columns = array.Length;
        //        }
        //    }

        //    // Add columns.
        //    for (int i = 0; i < columns; i++)
        //    {
        //        table.Columns.Add();
        //    }

        //    // Add rows.
        //    foreach (var array in list)
        //    {
        //        table.Rows.Add(array);
        //    }

        //    return table;
        //}
    }
}
