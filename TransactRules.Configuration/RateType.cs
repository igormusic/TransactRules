using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactRules.Core.Entities;

namespace TransactRules.Configuration
{
    public class RateType:Entity
    {
        public virtual AccountType AccountType { get; set; }
        public virtual string Name { get; set; }
    }
}
