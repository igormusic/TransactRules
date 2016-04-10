using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactRules.Core.Entities;

namespace TransactRules.Configuration
{
    public class TransactionRuleType:Entity
    {
        public virtual TransactionType TransactionType { get; set; }
        public virtual PositionType PositionType { get; set; }
        public virtual TransactionOperation TransactionOperation { get; set; }
    }
}
