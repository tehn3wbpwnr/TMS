using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Classes;

namespace TMS
{
    internal class Planner : UserRole
    {
        //attributes
        private int completedOrders;
        //methods
        public void SimulateTime(int days)
        {

        }
        public void OrderSummary()
        {

        }
        
        public void InvoiceSummary()
        {

        }

        public bool ConfirmOrder(object order)
        {
            bool orderConfirmed = false;
            return orderConfirmed;
        }

        public bool ReceiveOrder(object order)
        {
            bool orderReceived = false;
            return orderReceived;
        }

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
