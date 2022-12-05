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

        static RateFee()
        {
            //FTL Rate
            rates.Add("4.985");
            //LTL Rate
            rates.Add("0.2995");
        }
    }
}
