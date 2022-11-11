using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
