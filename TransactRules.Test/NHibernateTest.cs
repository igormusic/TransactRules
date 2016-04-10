using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactRules.Core.NHibernateDriver;

namespace TransactRules.Test
{
    [TestClass]
    public class NHibernateTest
    {
        [TestMethod]
        public void CreateDatabase()
        {
            var mapper = new NHibernateReflectionMapper();

            var sql = mapper.ExportSchema();
        }
    }
}
