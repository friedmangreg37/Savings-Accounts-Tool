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
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required, Column(TypeName = "varchar"), StringLength(25)]
        public string Name { get; set; }

        public ICollection<SavingsFund> Funds { get; set; }

        [Display(Name = "Balance"), DataType(DataType.Currency)]
        public decimal SumOfFunds { get { return Funds.Sum(f => f.Balance); } }

        public SavingsAccount()
        {
            Funds = new List<SavingsFund>();
        }
    }
}