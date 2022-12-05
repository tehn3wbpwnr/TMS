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
        public static decimal FTLRates;
        public static decimal LTLRates;
        public static decimal FTLMarkup;
        public static decimal LTLMarkup; 

        static RateFee()
        {
            FTLRates = (decimal)4.985;
            LTLRates = (decimal)0.2995;
            FTLMarkup = (decimal)0.08;
            LTLMarkup = (decimal)0.05;
        }
    }
}
