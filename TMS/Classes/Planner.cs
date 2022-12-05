/*
 * File         : Planner.cs
 * Project      : Milestone 4
 * Programmer   : Alex Silveira, Emanuel Juracic, Josh Moore
 * First Version: nov 10 2022
 * Description  : This file contains planner class related things
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Classes;

namespace TMS
{
    // Name     : Order
    // Purpose  : The purpose of this class is to hold planner realted methods/properties
    internal class Planner : UserRole
    {
        //methods
        /*
         * Method      : bool isEastBound(Order inprogressOrder)
         * Description : This is a method used for determining the direction of a trip.
         * Parameters  : Order inprogressOrder 
         * Returns     : bool: indicates true for east bound and false for westbound
        */
        public bool isEastBound(Order inprogressOrder)
        {
            //figure out if trip is going east or west
            bool eastTrip = true;
            foreach (City city in RouteTable.corridor)
            {
                if (city.city == inprogressOrder.Origin)
                {
                    //origin found first
                    break;
                }
                else if (city.city == inprogressOrder.Destination)
                {
                    //destination found first
                    eastTrip = false;
                    break;
                }
            }
            return eastTrip;
        }
    }
}
