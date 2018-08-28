using Budgeting.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Budgeting.Validators
{
    public class TransactionValidator
    {
        public const string NO_SAVINGS_FUND_ERROR_MESSAGE = "Transaction has no Savings Fund associated with it";
        public const string INSUFFICIENT_FUNDS_ERROR_MESSAGE = "Cannot withdraw more than current balance";

        public bool HasErrors
        {
            get { return ErrorMessages.Any(); }
        }

        public List<string> ErrorMessages = new List<string>();

        public virtual void Validate(Transaction transaction)
        {
            ErrorMessages.Clear();

            if (transaction.SavingsFund == null)
            {
                ErrorMessages.Add(NO_SAVINGS_FUND_ERROR_MESSAGE);
                return;
            }

            if (transaction.Type == Transaction.TransactionType.Withdrawal &&
                transaction.Amount > transaction.SavingsFund.Balance)
            {
                ErrorMessages.Add(INSUFFICIENT_FUNDS_ERROR_MESSAGE);
            }
        }
    }
}