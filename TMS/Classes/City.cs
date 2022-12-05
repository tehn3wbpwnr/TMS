/*
 * File         : City.cs
 * Project      : Milestone 4
 * Programmer   : Alex Silveira, Emanuel Juracic, Josh Moore
 * First Version:
 * Description  : This class is basically a container for city information that is later held in a list
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Classes
{
    // Name     : City
    // Purpose  : The purpose of this class is to essentially be a partial container for citys, which are later held in a static list
    internal class City
    {
        public string city;
        public int distance;//distance is to east city
        public double time;//time is to east city
        public string westCity;
        public string eastCity;

        public City(string city, int distance, double time, string westCity, string eastCity)
        {
            this.city = city;
            this.distance = distance;
            this.time = time;
            this.westCity = westCity;
            this.eastCity = eastCity;
        }
    }
}
