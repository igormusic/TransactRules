using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Core.Entities;

namespace TransactRules.Configuration
{
    public class PartyType:Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}
