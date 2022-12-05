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
        public static List<string> rates = new List<string>();
        public static decimal FTLMarkUp = 0.08M;
        public static decimal LTLMarkUp = 0.05M;
        public static decimal salesTax = 0.13M;

        static RateFee()
        {
            //FTL Rate
            rates.Add("4.985");
            //LTL Rate
            rates.Add("0.2995");
        }
    }
}
