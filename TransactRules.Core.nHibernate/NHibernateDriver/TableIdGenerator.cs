using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Id;
using NHibernate.Dialect;
using NHibernate.Type;

namespace TransactRules.nHibernate.NHibernateDriver
{
    public class NHibIdGenerator : TableHiLoGenerator
    {
        public override void Configure(IType type, IDictionary<string, string> parms, Dialect dialect)
        {
            if (!parms.ContainsKey("table"))
                parms.Add("table", "NextKey");

            if (!parms.ContainsKey("column"))
                parms.Add("column", "NextHiValue");

            if (!parms.ContainsKey("max_lo"))
                parms.Add("max_lo", "100");

            if (!parms.ContainsKey("where"))
                parms.Add("where", string.Format("EntityName='{0}'", parms["target_table"]));

            base.Configure(type, parms, dialect);
        }
    }
}
