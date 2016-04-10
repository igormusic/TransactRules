using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Configuration;
using TransactRules.Core.Entities;

namespace TransactRules.Configuration
{
    public class PartyRoleType:Entity
    {
        public virtual AccountType AccountType { get; set; }
        public virtual PartyType PartyType { get; set; }
        public virtual string Name { get; set; }
    }
}
