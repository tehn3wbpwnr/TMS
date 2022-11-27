using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    internal class Admin : UserRole
    {
        //methods
        public void InitiateBackup()
        {

        }

        public void ReviewLogs()
        {

        }

        public bool AddCarrier(object carrier)
        {
            bool carrierAdded = false;
            return carrierAdded;
        }

        public bool UpdateCarrier(object carrier)
        {
            bool carrierUpdated = false;
            return carrierUpdated;
        }

        public bool DeleteCarrier(object carrier)
        {
            bool carrierDeleted = false;
            return carrierDeleted;
        }

        public void TMSConfig(string key, string value)
        {

        }
    }
}
