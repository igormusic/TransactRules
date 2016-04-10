
using System;
using System.Linq;
using System.Collections.Generic;
using TransactRules.Core.Utilities;
using TransactRules.Configuration;
using TransactRules.Runtime;
using TransactRules.Runtime.Accounts;
using TransactRules.Runtime.Schedules;
using TransactRules.Runtime.Values;
using TransactRules.Runtime.Rates;


namespace TransactRules.Runtime
{
    public class LoanGiven : TransactionClient
    {
        public override void Initialize()
        {

            Schedule localAccrualSchedule = new Schedule();
            localAccrualSchedule.ScheduleType = "AccrualSchedule";
            localAccrualSchedule.BusinessDayCalculator = Account.BusinessDayCalculator;
            SetAccrualScheduleCalculatedProperties(localAccrualSchedule);
            Account.Schedules.Add(localAccrualSchedule);

            Schedule localInterestSchedule = new Schedule();
            localInterestSchedule.ScheduleType = "InterestSchedule";
            localInterestSchedule.BusinessDayCalculator = Account.BusinessDayCalculator;
            SetInterestScheduleDefaultProperties(localInterestSchedule);
            Account.Schedules.Add(localInterestSchedule);

            Schedule localRedemptionSchedule = new Schedule();
            localRedemptionSchedule.ScheduleType = "RedemptionSchedule";
            localRedemptionSchedule.BusinessDayCalculator = Account.BusinessDayCalculator;
            SetRedemptionScheduleDefaultProperties(localRedemptionSchedule);
            Account.Schedules.Add(localRedemptionSchedule);

            OptionValue localAccrualOption = new OptionValue();
            localAccrualOption.OptionType = "AccrualOption";
            localAccrualOption.OptionValues = TransactRules.Calculations.AccrualCalculation.AccrualOptions();
            Account.Options.Add(localAccrualOption);

            RateValue localInterestRate = new RateValue();
            localInterestRate.RateType = "InterestRate";
            Account.Rates.Add(localInterestRate);

        }


        private Position _ConversionInterest;

        public Position ConversionInterest
        {
            get
            {
                if (_ConversionInterest == null)
                {
                    _ConversionInterest = Account.GetPosition("ConversionInterest");
                }

                return _ConversionInterest;
            }
        }

        private Position _EarlyRedemptionFee;

        public Position EarlyRedemptionFee
        {
            get
            {
                if (_EarlyRedemptionFee == null)
                {
                    _EarlyRedemptionFee = Account.GetPosition("EarlyRedemptionFee");
                }

                return _EarlyRedemptionFee;
            }
        }

        private Position _InterestAccrued;

        public Position InterestAccrued
        {
            get
            {
                if (_InterestAccrued == null)
                {
                    _InterestAccrued = Account.GetPosition("InterestAccrued");
                }

                return _InterestAccrued;
            }
        }

        private Position _InterestCapitalized;

        public Position InterestCapitalized
        {
            get
            {
                if (_InterestCapitalized == null)
                {
                    _InterestCapitalized = Account.GetPosition("InterestCapitalized");
                }

                return _InterestCapitalized;
            }
        }

        private Position _Principal;

        public Position Principal
        {
            get
            {
                if (_Principal == null)
                {
                    _Principal = Account.GetPosition("Principal");
                }

                return _Principal;
            }
        }


        private AmountValue _RedemptionAmount;

        public AmountValue RedemptionAmount
        {
            get
            {
                if (_RedemptionAmount == null)
                {
                    _RedemptionAmount = Account.GetAmount("RedemptionAmount");
                }

                return _RedemptionAmount;
            }
        }

        private AmountValue _AdditionalAdvanceAmount;

        public AmountValue AdditionalAdvanceAmount
        {
            get
            {
                if (_AdditionalAdvanceAmount == null)
                {
                    _AdditionalAdvanceAmount = Account.GetAmount("AdditionalAdvanceAmount");
                }

                return _AdditionalAdvanceAmount;
            }
        }

        private AmountValue _ConversionInterestAmount;

        public AmountValue ConversionInterestAmount
        {
            get
            {
                if (_ConversionInterestAmount == null)
                {
                    _ConversionInterestAmount = Account.GetAmount("ConversionInterestAmount");
                }

                return _ConversionInterestAmount;
            }
        }

        private AmountValue _AdvanceAmount;

        public AmountValue AdvanceAmount
        {
            get
            {
                if (_AdvanceAmount == null)
                {
                    _AdvanceAmount = Account.GetAmount("AdvanceAmount");
                }

                return _AdvanceAmount;
            }
        }


        private DateValue _AccrualStart;

        public DateValue AccrualStart
        {
            get
            {
                if (_AccrualStart == null)
                {
                    _AccrualStart = Account.GetDate("AccrualStart");
                }

                return _AccrualStart;
            }
        }

        private DateValue _StartDate;

        public DateValue StartDate
        {
            get
            {
                if (_StartDate == null)
                {
                    _StartDate = Account.GetDate("StartDate");
                }

                return _StartDate;
            }
        }

        private DateValue _EndDate;

        public DateValue EndDate
        {
            get
            {
                if (_EndDate == null)
                {
                    _EndDate = Account.GetDate("EndDate");
                }

                return _EndDate;
            }
        }


        private OptionValue _AccrualOption;

        public OptionValue AccrualOption
        {
            get
            {
                if (_AccrualOption == null)
                {
                    _AccrualOption = Account.GetOption("AccrualOption");
                }

                return _AccrualOption;
            }
        }


        private RateValue _InterestRate;

        public RateValue InterestRate
        {
            get
            {
                if (_InterestRate == null)
                {
                    _InterestRate = Account.GetRate("InterestRate");
                }

                return _InterestRate;
            }
        }

        public override void StartOfDay()
        {
            var valueDate = SessionState.Current.ValueDate;
            if (StartDate.IsDue())
            {
                var amount = decimal.Round(AdvanceAmount, 2);
                CreateTransaction(transactionType: "Advance", amount: amount);
            }
            if (Redemptions.ContainsKey(valueDate))
            {
                var instalment = Redemptions[valueDate];

                CreateTransaction(transactionType: instalment.TransactionType, amount: instalment.Amount);
            }
        }

        public override void EndOfOfDay()
        {
            var valueDate = SessionState.Current.ValueDate;
            if (AccrualSchedule.IsDue())
            {
                var amount = TransactRules.Calculations.AccrualCalculation.InterestAccrued(accrualOption: AccrualOption, principal: Principal, rate: InterestRate, valueDate: TransactRules.Core.Utilities.SessionState.Current.ValueDate);
                CreateTransaction(transactionType: "InterestAccrued", amount: amount);
            }
            if (InterestSchedule.IsDue())
            {
                var amount = decimal.Round(InterestAccrued, 2);
                CreateTransaction(transactionType: "InterestCapitalized", amount: amount);
            }
        }

        public override void OnDataChanged()
        {
            UpdateSchedules();
        }

        private void UpdateSchedules()
        {
            SetAccrualScheduleCalculatedProperties(AccrualSchedule);
        }


        private Schedule _AccrualSchedule;

        public Schedule AccrualSchedule
        {
            get
            {
                if (_AccrualSchedule == null)
                {
                    _AccrualSchedule = Account.GetSchedule("AccrualSchedule");
                }

                return _AccrualSchedule;
            }
        }

        private Schedule _InterestSchedule;

        public Schedule InterestSchedule
        {
            get
            {
                if (_InterestSchedule == null)
                {
                    _InterestSchedule = Account.GetSchedule("InterestSchedule");
                }

                return _InterestSchedule;
            }
        }

        private Schedule _RedemptionSchedule;

        public Schedule RedemptionSchedule
        {
            get
            {
                if (_RedemptionSchedule == null)
                {
                    _RedemptionSchedule = Account.GetSchedule("RedemptionSchedule");
                }

                return _RedemptionSchedule;
            }
        }

        protected virtual void SetAccrualScheduleCalculatedProperties(Schedule AccrualSchedule)
        {
            AccrualSchedule.StartDate = StartDate;
            AccrualSchedule.Interval = 1;
            AccrualSchedule.EndType = ScheduleEndType.NoEnd;
            AccrualSchedule.BusinessDayCalculation = BusinessDayCalculation.AnyDay;
            AccrualSchedule.Frequency = ScheduleFrequency.Daily;

        }

        protected virtual void SetInterestScheduleDefaultProperties(Schedule InterestSchedule)
        {
            InterestSchedule.BusinessDayCalculation = BusinessDayCalculation.AnyDay;
            InterestSchedule.Frequency = ScheduleFrequency.Monthly;
            InterestSchedule.Interval = 1;
            InterestSchedule.EndType = ScheduleEndType.EndDate;
        }

        protected virtual void SetRedemptionScheduleDefaultProperties(Schedule RedemptionSchedule)
        {
            RedemptionSchedule.BusinessDayCalculation = BusinessDayCalculation.AnyDay;
            RedemptionSchedule.Frequency = ScheduleFrequency.Monthly;
            RedemptionSchedule.Interval = 1;
            RedemptionSchedule.EndType = ScheduleEndType.EndDate;
        }

        public override void ProcessTransaction(Transaction transaction)
        {
            switch (transaction.TransactionType)
            {
                case "AdditionalAdvance":
                    Principal.Value += transaction.Amount;
                    break;
                case "Advance":
                    Principal.Value += transaction.Amount;
                    break;
                case "ConversionInterest":
                    ConversionInterest.Value += transaction.Amount;
                    break;
                case "EarlyRedemptionFee":
                    EarlyRedemptionFee.Value += transaction.Amount;
                    break;
                case "FXResultInterest":
                    InterestAccrued.Value += transaction.Amount;
                    break;
                case "FXResultPrincipal":
                    Principal.Value += transaction.Amount;
                    break;
                case "InterestAccrued":
                    InterestAccrued.Value += transaction.Amount;
                    break;
                case "InterestCapitalized":
                    Principal.Value += transaction.Amount;
                    InterestAccrued.Value -= transaction.Amount;
                    InterestCapitalized.Value += transaction.Amount;
                    break;
                case "InterestPayment":
                    InterestAccrued.Value -= transaction.Amount;
                    break;
                case "Redemption":
                    Principal.Value -= transaction.Amount;
                    break;
            }
        }

        private Dictionary<DateTime, Instalment> _Redemptions;

        public Dictionary<DateTime, Instalment> Redemptions
        {
            get
            {
                if (_Redemptions == null)
                {
                    var instalments = Account.GetInstalments("Redemptions");

                    if (instalments.Count() == 0)
                    {
                        InitializeRedemptions();
                        instalments = Account.GetInstalments("Redemptions");
                    }

                    _Redemptions = instalments.ToDictionary(i => i.ValueDate);
                }
                return _Redemptions;
            }
        }

        private void InitializeRedemptions()
        {
            foreach (var date in RedemptionSchedule.GetAllDates())
            {
                Account.Instalments.Add(new Instalment { InstalmentType = "Redemptions", TransactionType = "Redemption", ValueDate = date });
            }
        }

        public void CalculateRedemptionsInstalments()
        {
            var amount = Solver.FindFunctionZero((value) => this.GetClosingBalanceForRedemptionsValue(value), (decimal)0, (decimal)1000000000000000, (decimal)0.01);
        }

        public decimal GetClosingBalanceForRedemptionsValue(decimal value)
        {
            foreach (var instalment in Redemptions.Values)
            {
                if (!instalment.HasFixedValue)
                    instalment.Amount = value;
            }

            var lastDate = RedemptionSchedule.GetAllDates().Last();

            Account.Snapshot();

            Forecast(lastDate);

            var result = Principal.Value;

            Account.RestoreSnapshot();

            return result;
        }
        public override void CalculateInstaments()
        {
            CalculateRedemptionsInstalments();
        }
    }
}