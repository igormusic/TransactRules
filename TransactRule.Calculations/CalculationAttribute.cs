using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransactRules.Calculations
{
    [AttributeUsageAttribute(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class CalculationAttribute : Attribute
    { 
    
    }
}
