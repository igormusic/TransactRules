using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransactRules.Configuration
{
    public enum BusinessDayCalculation
    {
        AnyDay,
        NextBusinessDay,
        PreviousBusinessDay,
        ClosestBusinessDayOrNext,
        NextBusinessDayThisMonthOrPrevious
    }
}
