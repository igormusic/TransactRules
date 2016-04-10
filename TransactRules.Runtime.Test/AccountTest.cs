using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactRules.Runtime.Accounts;
using TransactRules.Runtime.CodeGen;
using TransactRules.Configuration;

namespace TransactRules.Runtime.Test
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void TestProcessTransaction()
        {
            var account = new Account();

            var client = AccountFactory.CreateTransactionClient(Utility.CreateSavingsAccountType(), account);

            client.ProcessTransaction(new Transaction { TransactionType = "Deposit", Amount = 100 });

            var currentPosition = client.Positions.FirstOrDefault(b => b.PositionType == "Current");

            Assert.AreEqual(100, currentPosition.Value);

        }

 
    }
}
