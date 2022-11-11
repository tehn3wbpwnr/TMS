using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    internal class Buyer : UserRole
    {
        //methods
        public object GetContract()
        {
            object contract = new object();
            return contract; //once contract class is implemented change this to maybe return the contract class or we could treat it as an object
        }

        public bool CreateOrder()
        {
            //changed to return bool instead of object
            bool orderCreated = false;
            return orderCreated;
        }

        public void ReviewOrder(object order)
        {

        }

        public object CreateInvoice(object order)
        {
            object invoice = new object();
            return invoice;
        }

        public void ExistingCustomers()
        {

        }

        public void NewCustomer(object customer)
        {

        }
    }
}
