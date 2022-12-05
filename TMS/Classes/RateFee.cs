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
        public static string FTLRates;
        public static string LTLRates;
        public static string FTLMarkup;
        public static string LTLMarkup; 

        static RateFee()
        {
            FTLRates = "4.985";
            LTLRates = "0.2995";
            FTLMarkup = "0.08";
            LTLMarkup = "0.05";
        }
    }
}
