using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactRules.Core.Entities;

namespace TransactRules.Configuration
{
    public class TransactionType : Entity
    {
        public virtual AccountType AccountType { get; set; }
        public virtual string Name { get; set; }
        public virtual bool HasMaximumPrecission { get; set; }
        public virtual IList<TransactionRuleType> TransactionRules { get; set; }
    }
}
