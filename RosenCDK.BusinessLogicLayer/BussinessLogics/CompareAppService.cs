using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;
using RosenCDK.Utilities;

namespace RosenCDK.BussinessLogics
{
    public class CompareAppService :DomainService,ICompareAppService
    {
        public string CalculateEndDate(string startDateRaw, double days, List<int> dayOff)
        {
//            DateTime endDate = DateTime.Parse(startDateRaw);
            DateTime endDate = DateCalculator.StringToDate(startDateRaw);
            endDate = endDate.AddDays(-1);
            var sign = Math.Sign(days);
            var unsignedDays = Math.Abs(days);
            for (var i = 0; i < unsignedDays; i++)
            {
                do
                {
                    endDate = endDate.AddDays(sign);
                }
                while (dayOff.Contains((int)endDate.DayOfWeek));
            }
            return endDate.ToShortDateString();
        }

        public bool Duplicate(int x, int[] y)
        {
            if (y.Contains(x))
                return true;

            return false;
        }

        public int[] GetMissingIntegers(int[] x, int[] y)
        {
            List<int> temp = new List<int>();

            for (int i = 0; i < y.Length; i++)
            {
                for (int j = 0; j < x.Length; j++)
                {
                    //If y[i] didnt match with any integer in x
                    //we will add that y[i] to array of missing intergers
                    if (x[j] != y[i] && j == x.Length - 1)
                    {
                        temp.Add(y[i]);
                    }
                    else if (x[j] == y[i])
                    {
                        break;
                    }
                }
            }

            return temp.ToArray();
        }

        public bool IsContainAny(int[] x, int[] y)
        {
            for (int i = 0; i < x.Length; i++)
            {
                if (Duplicate(x[i], y))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsMissingCompetencies(int[] sourceCompetence, int[] targetCompetence)
        {
            List<int> missedCompetencies = missingCompetencies(sourceCompetence, targetCompetence).ToList();
            if (missedCompetencies.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int[] missingCompetencies(int[] sourceCompetence, int[] targetCompetence)
        {
            return targetCompetence.Except(sourceCompetence).ToArray();
        }
    }
}
