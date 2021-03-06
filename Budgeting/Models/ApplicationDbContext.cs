﻿using System.Data.Entity;
using Budgeting.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Budgeting.Models
{
    public interface IApplicationDbContext
    {
        IDbSet<SavingsAccount> SavingsAccounts { get; set; }
        IDbSet<SavingsFund> SavingsFunds { get; set; }
        //IDbSet<Transaction> Transactions { get; set; }

        int SaveChanges();
        void SetEntityState(object entity, EntityState state);
        void Dispose();
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            base.OnModelCreating(modelBuilder);
        }

        public void SetEntityState(object entity, EntityState state)
        {
            Entry(entity).State = state;
        }

        public IDbSet<SavingsAccount> SavingsAccounts { get; set; }
        public IDbSet<SavingsFund> SavingsFunds { get; set; }
        //public IDbSet<Transaction> Transactions { get; set; }
    }
}