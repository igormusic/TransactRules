using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransactRules.Calculations
{
    public class AccrualCalculation
    {
        [Calculation]
        public static decimal InterestAccrued(
            [Domain("AccrualOptions")]string accrualOption, 
            Decimal principal, 
            Decimal rate, 
            DateTime valueDate) 
        {
            decimal accrued;
            decimal divisor= 365;

            switch (accrualOption)
            { 
                case "Actual":
                    if (DateTime.IsLeapYear(valueDate.Year))
                    {
                        divisor = 366;
                    }
                    break;
                case "360":
                    divisor = 360;
                    break;
                case "30/360":
                    divisor = 360;
                    break;
            }

            accrued = principal * rate / divisor;

            if (accrualOption == "30/360" && IsLastDayOfMonth(valueDate))
            {
                switch (valueDate.Day)
                {
                    case 28:
                        accrued *= 3;
                        break;
                    case 29:
                        accrued *= 2;
                        break;
                    case 31:
                        accrued = 0;
                        break;
                }
            }

            return accrued;

            //var a = InterestAccrued(rate: 1, valueDate: DateTime.Now, accrualOption: "360", principal: 100);
        }

        public static IEnumerable<string> AccrualOptions() {
            return new List<string>(){
                "Actual",
                "360",
                "365",
                "30/360"
            };
        }

        private static bool IsLastDayOfMonth(DateTime valueDate){
            return valueDate.Day == DateTime.DaysInMonth(valueDate.Year, valueDate.Month);
        }
    }


}
