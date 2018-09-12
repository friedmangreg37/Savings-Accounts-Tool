using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budgeting.Models
{
    public class SavingsFund
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Amount/Month"), DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be greater than or equal to $0")]
        public decimal AmountPerMonth { get; set; }

        [DataType(DataType.Currency), Range(0, double.MaxValue, ErrorMessage = "Balance must be greater than or equal to $0")]
        public decimal Balance { get; set; }

        [Required, ForeignKey("Account")]
        public int AccountID { get; set; }

        public SavingsAccount Account { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public virtual void Withdraw(decimal amount)
        {
            Balance -= amount;
        }
    }
}