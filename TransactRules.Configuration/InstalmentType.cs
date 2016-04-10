using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Core.Entities;

namespace TransactRules.Configuration
{
    public class InstalmentType:Entity
    {
        public virtual AccountType AccountType { get; set; }
        public virtual string Name { get; set; }
        public virtual ScheduledTransactionTiming Timing { get; set; }
        public virtual ScheduleType ScheduleType { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual PositionType PrincipalPositionType { get; set; }
        //required for amortization calculations
        public virtual PositionType InterestAccrued { get; set; }
        public virtual PositionType InterestACapitalized { get; set; }

    }
}
