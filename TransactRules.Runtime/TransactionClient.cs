using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Runtime.Accounts;
using TransactRules.Runtime.Values;
using TransactRules.Core.Utilities;
using TransactRules.Configuration;

namespace TransactRules.Runtime
{
    public abstract class TransactionClient
    {
        public Account Account { get; set; }
        public virtual IList<Transaction> Transactions
        {
            get
            {
                return Account.Transactions;
            }
        }

        public virtual IList<Position> Positions
        {
            get
            {
                return Account.Positions;
            }
        }

        public abstract void Initialize();

        public abstract void ProcessTransaction(Transaction transaction);

        public abstract void StartOfDay();
        public abstract void EndOfOfDay();

        public abstract void OnDataChanged();

        public abstract void CalculateInstaments();

        public Transaction CreateTransaction(string transactionType, decimal amount) 
        {
            var transaction = new Transaction { TransactionType = transactionType, Amount = amount };
            ProcessTransaction(transaction);
            Transactions.Add(transaction);

            return transaction;
        }

        public void Forecast(DateTime futureDate)
        {
            var originalValueDate = SessionState.Current.ValueDate;

            var iterator = originalValueDate;

            if (!Account.IsActive)
            {
                StartOfDay();
            }

            while (SessionState.Current.ValueDate <= futureDate)
            {
                EndOfOfDay();

                SessionState.Current.ValueDate = SessionState.Current.ValueDate.AddDays(1);

                StartOfDay();
            }

            SessionState.Current.ValueDate = originalValueDate;
        }

        public void SetFutureInstalmentValue(string instalmentType, ScheduledTransactionTiming timing, decimal value)
        { 
            foreach (var instalment in Account.GetInstalments(instalmentType))
            {
                if (!instalment.HasFixedValue)
                {
                    if ( (timing == ScheduledTransactionTiming.StartOfDay && instalment.ValueDate > SessionState.Current.ValueDate)
                        || (timing == ScheduledTransactionTiming.EndOfDay && instalment.ValueDate >= SessionState.Current.ValueDate))
                    {
                        instalment.Amount = value;
                    }                    
                }
            }
        }

    }
}
