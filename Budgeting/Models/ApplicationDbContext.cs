using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Budgeting.Models
{
    public interface IApplicationDbContext
    {
        IDbSet<SavingsAccount> SavingsAccounts { get; set; }
        IDbSet<SavingsFund> SavingsFunds { get; set; }
        //IDbSet<Transaction> Transactions { get; set; }

        int SaveChanges();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<SavingsAccount> SavingsAccounts { get; set; }
        public IDbSet<SavingsFund> SavingsFunds { get; set; }
        //public IDbSet<Transaction> Transactions { get; set; }
    }

    public class MockApplicationDbContext : IApplicationDbContext
    {
        public IDbSet<SavingsAccount> SavingsAccounts { get; set; }
        public IDbSet<SavingsFund> SavingsFunds { get; set; }
        //public IDbSet<Transaction> Transactions { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}