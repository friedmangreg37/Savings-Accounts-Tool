using Budgeting.Validators;
using System.ComponentModel.DataAnnotations;

namespace Budgeting.Models
{
    public class Transaction
    {
        public enum TransactionType
        {
            Deposit, Withdrawal
        }

        public int Id { get; set; }

        public TransactionType Type { get; set; }

        [Required, DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than or equal to $0")]
        public decimal Amount { get; set; }

        [Required]
        public int FundId { get; set; }
        public virtual SavingsFund SavingsFund { get; set; }

        public virtual TransactionValidator GetTransactionValidator()
        {
            return new TransactionValidator();
        }

        public void Apply()
        {
            GetTransactionValidator().Validate(this);

            switch (Type)
            {
                case TransactionType.Deposit:
                    SavingsFund.Deposit(Amount);
                    break;
                case TransactionType.Withdrawal:
                    SavingsFund.Withdraw(Amount);
                    break;
                default:
                    break;
            }
        }
    }
}