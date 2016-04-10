using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Core.Entities;
using TransactRules.Core.Attributes;

namespace TransactRules.Runtime.Values
{
    public class OptionValue:Entity
    {
        public virtual Entity Consumer { get; set; }
        public virtual string Value { get; set; }
        public virtual string OptionType { get; set; }

        public static implicit operator string(OptionValue optionValue)
        {
            return optionValue.Value;
        }

        [NonPersistent]
        public IEnumerable<String> OptionValues { get; set; }
    }
}
