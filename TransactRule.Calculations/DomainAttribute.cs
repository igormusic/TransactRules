using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransactRules.Calculations
{
    [AttributeUsageAttribute(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class DomainAttribute : Attribute
    {
        private string _domain;

        public DomainAttribute(string domain) {
            _domain = domain;
        }

        public string Domain { 
            get{
                return _domain;
            }
        }
    }
}
