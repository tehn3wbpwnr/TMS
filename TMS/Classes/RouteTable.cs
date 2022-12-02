using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Classes
{
    static internal class RouteTable
    {
        public static List<City> corridor = new List<City>();

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
    }
}
