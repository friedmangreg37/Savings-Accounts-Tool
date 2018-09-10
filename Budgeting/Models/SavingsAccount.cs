using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Budgeting.Models
{
    public class SavingsAccount
    {
        public const string FUND_TOO_HIGH_ERROR_MESSAGE = "Fund balance cannot be greater than unallocated funds in account. Please either decrease fund balance or increase account balance";

        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required, Column(TypeName = "varchar"), StringLength(25)]
        public string Name { get; set; }

        [DataType(DataType.Currency), Range(0, double.MaxValue, ErrorMessage = "Balance must be greater than or equal to $0")]
        public decimal Balance { get; set; }

        public ICollection<SavingsFund> Funds { get; set; }

        [DataType(DataType.Currency)]
        public decimal SumOfFunds { get { return Funds.Sum(f => f.Balance); } }

        [Display(Name = "Unallocated Funds"), DataType(DataType.Currency)]
        public virtual decimal UnallocatedFunds { get { return Balance - SumOfFunds; } }

        public SavingsAccount()
        {
            Funds = new List<SavingsFund>();
        }

        public void CreateNewFund(SavingsFund fund)
        {
            if (fund.Balance > UnallocatedFunds)
                throw new ArgumentException(FUND_TOO_HIGH_ERROR_MESSAGE);

            Funds.Add(fund);
        }
    }
}