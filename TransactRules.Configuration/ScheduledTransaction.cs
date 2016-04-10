using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace TransactRules.Configuration
{
    public enum ScheduledTransactionTiming { 
        StartOfDay,
        EndOfDay
    }

    public class ScheduledTransaction:Entity
    {
        public virtual AccountType AccountType { get; set; }
        public virtual string Name { get; set; }
        public virtual ScheduledTransactionTiming Timing { get; set; }
        public virtual ScheduleType ScheduleType { get; set; }
        public virtual DateType DateType { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        [StringLength(64000)]
        public virtual string AmountExpression { get; set; }
        public virtual int Sequence { get; set; }
    }
}
