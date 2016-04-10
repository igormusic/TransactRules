using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactRules.Configuration;
using TransactRules.Runtime.Accounts;
using TransactRules.Runtime.CodeGen;
using TransactRules.Runtime.Values;
using TransactRules.Runtime.Schedules;
using TransactRules.Runtime.Rates;
using TransactRules.Core.Utilities;
using TransactRules.Configuration.Data;
using TransactRules.Core.NHibernateDriver;

namespace TransactRules.Runtime.Test
{
    [TestClass]
    public class LoanGiven
    {
        


        [TestMethod]
        public void TestTransactions()
        {
            var account = new Account();

            var client = AccountFactory.CreateTransactionClient(Utility.CreateLoanGivenAccountType(), account);

            client.ProcessTransaction(new Transaction { TransactionType = "Advance", Amount = 100 });

            var currentPosition = client.Positions.FirstOrDefault(b => b.PositionType == "Principal");

            Assert.AreEqual(100, currentPosition.Value);

        }

        [TestMethod]
        public void TestSchedules()
        {
            DateTime startDate = new DateTime(2013, 3, 8);
            DateTime endDate = startDate.AddYears(25);
            Calendar calendar = Utility.CreateEuroZoneCalendar();
           
            var account = CreateLoanGivenAccount(startDate, endDate,calendar);

            var client = AccountFactory.CreateTransactionClient(Utility.CreateLoanGivenAccountType(), account);

            client.Initialize();

            var accrualSchedule = account.GetSchedule("AccrualSchedule");
            var interestSchedule = account.GetSchedule("InterestSchedule");

            DateTime interestStart =  new DateTime(2013, 3, 31);

            interestSchedule.StartDate = interestStart;
            interestSchedule.EndDate = endDate;
            interestSchedule.IncludeDates.Add(new ScheduleDate { Value = endDate });

            var accrualDates = accrualSchedule.GetAllDates(endDate);

            Assert.AreEqual(startDate, accrualDates.First());
            Assert.AreEqual(endDate, accrualDates.Last());
            Assert.AreEqual(endDate.Subtract(startDate).Days+1, accrualDates.Count());

            var interestDates = interestSchedule.GetAllDates(endDate);

            Assert.AreEqual(interestStart, interestDates.First());
            Assert.AreEqual(new DateTime(2013,4,30) , interestDates.Skip(1).First());
            Assert.AreEqual(new DateTime(2013, 5, 31), interestDates.Skip(2).First());
            Assert.AreEqual(new DateTime(2014, 2, 28), interestDates.Skip(11).First());
            Assert.AreEqual(endDate, interestDates.Last());
        }

        [TestMethod]
        public void TestOptions()
        {
            DateTime startDate = new DateTime(2013, 3, 8);
            DateTime endDate = startDate.AddYears(25);
            
            var account = CreateLoanGivenAccount(startDate, endDate, Utility.CreateEuroZoneCalendar());

            var client = AccountFactory.CreateTransactionClient(Utility.CreateLoanGivenAccountType(), account);

            client.Initialize();

            var accrualOption = account.GetOption("AccrualOption");

            Assert.AreEqual(4, accrualOption.OptionValues.Count());
            Assert.AreEqual("Actual", accrualOption.OptionValues.First());
            Assert.AreEqual("30/360", accrualOption.OptionValues.Last());
        }

        private static Account CreateLoanGivenAccount(DateTime startDate, DateTime endDate,IBusinessDayCalculator businessDayCalculator) {
            var account = new Account();

            account.Dates.Add(new DateValue { DateType = "StartDate", Value = startDate });
            account.Dates.Add(new DateValue { DateType = "AccrualStart", Value = startDate });
            account.Dates.Add(new DateValue { DateType = "EndDate", Value = endDate });

            account.Amounts.Add(new AmountValue { AmountType = "AdvanceAmount", Value = 624000 });
            account.Rates.Add(new RateValue { RateType = "InterestRate", Value = (decimal)3.04/100, ValueDate = startDate });

            account.BusinessDayCalculator = businessDayCalculator;

            return account;
        }


        [TestMethod]
        public void TestForecast()
        {
            DateTime startDate = new DateTime(2013, 3, 8);
            DateTime endDate = startDate.AddYears(25);
            Calendar calendar = Utility.CreateEuroZoneCalendar();

            SessionState.Current.ValueDate = startDate;

            var account = CreateLoanGivenAccount(startDate, endDate, calendar);

            var client = AccountFactory.CreateTransactionClient(Utility.CreateLoanGivenAccountType(), account);

            //var client = new TransactRules.Runtime.LoanGiven { Account = account };

            client.Initialize();

            var accrualSchedule = account.GetSchedule("AccrualSchedule");
            var interestSchedule = account.GetSchedule("InterestSchedule");

            DateTime interestStart = new DateTime(2013, 3, 31);

            interestSchedule.StartDate = interestStart;
            interestSchedule.EndDate = endDate;
            interestSchedule.IncludeDates.Add(new ScheduleDate { Value = endDate });

            var accrualOption = account.GetOption("AccrualOption");

            accrualOption.Value = "365";

            client.Forecast(endDate);

            Assert.AreEqual((decimal)1333778.93, account.GetPosition("Principal").Value);
            Assert.IsTrue(account.GetPosition("InterestAccrued").Value<(decimal)0.005);
            Assert.AreEqual((decimal)709778.93, account.GetPosition("InterestCapitalized").Value);

        }

        [TestMethod]
        public void TestPaymentCalc()
        {
            DateTime startDate = new DateTime(2013, 3, 8);
            DateTime endDate = startDate.AddYears(25);
            Calendar calendar = Utility.CreateEuroZoneCalendar();

            SessionState.Current.ValueDate = startDate;

            var account = CreateLoanGivenAccount(startDate, endDate, calendar);

            var client = AccountFactory.CreateTransactionClient(Utility.CreateLoanGivenAccountType(), account);

            //var client = new TransactRules.Runtime.LoanGiven { Account = account };

            client.Initialize();

            var accrualSchedule = account.GetSchedule("AccrualSchedule");
            var interestSchedule = account.GetSchedule("InterestSchedule");
            var redemptionSchedule = account.GetSchedule("RedemptionSchedule");

            DateTime interestStart = new DateTime(2013, 3, 31);

            interestSchedule.StartDate = interestStart;
            interestSchedule.EndDate = endDate;
            interestSchedule.IncludeDates.Add(new ScheduleDate { Value = endDate });

            redemptionSchedule.StartDate = interestStart;
            redemptionSchedule.EndDate = endDate;
            redemptionSchedule.IncludeDates.Add(new ScheduleDate { Value = endDate });

            var accrualOption = account.GetOption("AccrualOption");

            accrualOption.Value = "365";

            client.CalculateInstaments();

            var instalments = account.GetInstalments("Redemptions");

            Assert.IsTrue(Math.Abs((decimal)2964.37 - instalments.First().Amount) <(decimal) 0.01);
            Assert.IsTrue(Math.Abs((decimal)2964.37 - instalments.Last().Amount) < (decimal)0.01);
        }


    }
}
