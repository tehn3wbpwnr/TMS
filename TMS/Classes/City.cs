using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Classes
{
    internal class City
    {
        public string city;
        public double distance;//distance is to east city
        public double time;//time is to east city
        public string westCity;
        public string eastCity;

        public City(string city, double distance, double time, string westCity, string eastCity)
        {
            this.city = city;
            this.distance = distance;
            this.time = time;
            this.westCity = westCity;
            this.eastCity = eastCity;
        }
    }
}
