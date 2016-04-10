using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactRules.Core.Entities;

namespace TransactRules.Configuration
{
    public class AmountType:Entity
    {
        public virtual AccountType AccountType { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsValueDated { get; set; }
    }
}
