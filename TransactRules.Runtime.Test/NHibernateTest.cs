using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactRules.Core.NHibernateDriver;
using TransactRules.Configuration.Data;

namespace TransactRules.Runtime.Test
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

        [TestMethod]
        public void TestCreateAccountTypes()
        {
            var accountType = Utility.CreateLoanGivenAccountType();

            var repository = new AccountTypeRepository { UnitOfWork = new UnitOfWork() };

            repository.Create(accountType);

            accountType = Utility.CreateSavingsAccountType();

            repository.Create(accountType);

        }
    }
}
