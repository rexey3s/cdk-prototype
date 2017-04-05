using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.Utilities
{
    public class DateCalculator
    {
        public const string DATE_FORMAT = "MM/dd/yyyy";

        public static DateTime StringToDate(string stringDate)
        {
            return DateTime.ParseExact(stringDate, DATE_FORMAT, CultureInfo.InvariantCulture);
        }

        public static int CalculateModuleTotalDays(double moduleDuration, double trainTime)
        {
            return (moduleDuration % trainTime != 0 ? ((int)(moduleDuration / trainTime) + 1) : (int)(moduleDuration / trainTime));
        }
    }
}
