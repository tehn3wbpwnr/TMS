using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    internal class Order
    {
        private string clientName;
        private int orderId;
        private int jobType;
        private int quantity;
        private string origin;
        private string destination;
        private bool complete;
        private int truckType;
        private int numOfTrips;

        public string ClientName { get { return clientName; } set { clientName = value; } }
        public int OrderId { get { return orderId; } set { orderId = value; } }
        public int JobType { get { return jobType; } set { jobType = value; } }
        public string Origin { get { return origin; } set { origin = value; } }
        public string Destination { get { return destination; } set { destination = value; } }
        public int TruckType { get { return truckType; } set { truckType = value; } }
        public bool Complete { get { return complete; } set { complete = value; } }
        public int NumOfTrips { get { return numOfTrips; } set { numOfTrips = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }


        public Order(string client, int jobType, int quantity, string origin, string destination, int vanType)
        {
            this.ClientName = client;
            this.JobType = jobType;
            this.Quantity = quantity;
            this.Origin = origin;
            this.Destination = destination;
            this.TruckType = vanType;
            this.Complete = false;
        }
    }
}
