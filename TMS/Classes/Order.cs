/*
 * File         : Order.cs
 * Project      : Milestone 4
 * Programmer   : Alex Silveira, Emanuel Juracic, Josh Moore
 * First Version:
 * Description  : This file contains class related information for the Order class
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    // Name     : Order
    // Purpose  : The purpose of this class is to enable a container for the storage of orders when pulled from the DB so an order can easily be manipulated in memory.
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
        private string carrier;
        private decimal carrierTotal;
        public string ClientName { get { return clientName; } set { clientName = value; } }
        public int OrderId { get { return orderId; } set { orderId = value; } }
        public int JobType { get { return jobType; } set { jobType = value; } }
        public string Origin { get { return origin; } set { origin = value; } }
        public string Destination { get { return destination; } set { destination = value; } }
        public int TruckType { get { return truckType; } set { truckType = value; } }
        public bool Complete { get { return complete; } set { complete = value; } }
        public int NumOfTrips { get { return numOfTrips; } set { numOfTrips = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }
        public string Carrier { get { return carrier; } set { carrier = value; } }
        public decimal CarrierTotal { get { return carrierTotal; } set { carrierTotal = value; } }

        /*
         * Method      : Order(string client, int jobType, int quantity, string origin, string destination, int vanType)
         * Description : This is a constructor for the for Order object this version is used during the initial construction of a new order which lacks a order id
         * Parameters  : string client, int jobType, int quantity, string origin, string destination, int vanType 
         * Returns     : N/A
        */
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
        /*
         * Method      : Order(int orderID, string client, int jobType, int quantity, string origin, string destination, int vanType)
         * Description : This is a constructor for the order object this version is used during an in process order and includes the orderid
         * Parameters  : int orderID, string client, int jobType, int quantity, string origin, string destination, int vanType 
         * Returns     : N/A
        */
        public Order(int orderID, string client, int jobType, int quantity, string origin, string destination, int vanType)
        {
            this.OrderId = orderID;
            this.ClientName = client;
            this.JobType = jobType;
            this.Quantity = quantity;
            this.Origin = origin;
            this.Destination = destination;
            this.TruckType = vanType;
            this.Complete = false;
        }

        /*
         * Method      : Order(int orderID, string client, int jobType, int quantity, string origin, string destination, int vanType, decimal carrierTotal, int numOfTrips)
         * Description : This is a constructor for the order object this version is used for completed orders containing a total and num of trips value
         * Parameters  : int orderID, string client, int jobType, int quantity, string origin, string destination, int vanType, decimal carrierTotal, int numOfTrips
         * Returns     : N/A
        */
        public Order(int orderID, string client, int jobType, int quantity, string origin, string destination, int vanType, decimal carrierTotal, int numOfTrips)
        {
            this.OrderId = orderID;
            this.ClientName = client;
            this.JobType = jobType;
            this.Quantity = quantity;
            this.Origin = origin;
            this.Destination = destination;
            this.TruckType = vanType;
            this.Complete = false;
            this.CarrierTotal = carrierTotal;
            this.numOfTrips = numOfTrips;
        }
    }
}
