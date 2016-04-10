using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace TransactRules.Configuration
{
    public class OptionType:Entity
    {
        public virtual AccountType AccountType { get; set; }
        public virtual string Name { get; set; }
        [StringLength(64000)]
        public virtual string OptionListExpression { get; set; }
    }
}
