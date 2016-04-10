//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using TransactRules.Runtime.Accounts;
//using TransactRules.Runtime.Schedules;

//namespace TransactRules.Runtime.ReferenceCode
//{
//    public class SavingsAccount:Account
//    {
//        #region AccountType

//        public class PositionType
//        {
//            public const string CurrentBalance = "CurrentBalance";
//            public const string InterestAccrued = "InterestAccrued";
//        }

//        public class TransactionType 
//        {
//            public const string Deposit = "Deposit";
//            public const string Withdrawal = "Withdrawal";
//            public const string InterestAccrued = "InterestAccrued";
//            public const string InterestCompounded = "InterestCompounded";
//            public const string InterestPaid = "InterestPaid";     
//        }

//        public class DateType 
//        {
                    
//        }

//        public class AmountType
//        { 
            
//        }

//        public class RateType
//        {

//        }

//        public class ScheduleType
//        {
//            public const string Accrual = "Accrual";
//            public const string Compounding = "Compounding";
//        }


//        #endregion

//        #region Positions

//        public decimal CurrentBalance {
//            get
//            {
//                return GetPosition(PositionType.CurrentBalance).Value;
//            }
//        }

//        public decimal InterestAccrued
//        {
//            get
//            {
//                return GetPosition(PositionType.InterestAccrued).Value;
//            }
//        }
//    #endregion

//    #region Dates


//    #endregion


//    #region Schedule
//        public Schedule AccrualSchedule { 
//            get {
//                return GetSchedule(ScheduleType.Accrual);
//            } 
//        }


//    #endregion
//    }
//}
