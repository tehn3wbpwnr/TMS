using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace TMS.Classes
{
    static internal class RateFee
    {

        public static decimal FTLMarkUp = 0.08M;
        public static decimal LTLMarkUp = 0.05M;

        public static decimal salesTax = 0.13M;

        public static decimal FTLRates = 4.985M;
        public static decimal LTLRates = 0.2295M;
 


        static RateFee()
        {

        }
    }
}
