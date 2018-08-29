using Budgeting.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Budgeting.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Deposit(int fundId)
        {
            var fund = db.SavingsFunds.Find(fundId);
            ViewBag.Fund = fund;
            ViewBag.UnallocatedFunds = db.SavingsAccounts.Find(fund.AccountID).UnallocatedFunds;

            return View();
        }

        [HttpPost]
        public ActionResult Deposit([Bind(Include = "Id,Amount,FundId")] Transaction transaction)
        {
            transaction.Type = Transaction.TransactionType.Deposit;

            if (!ModelState.IsValid) return View();

            transaction.SavingsFund = db.SavingsFunds.Find(transaction.FundId);
            if (transaction.SavingsFund == null) return View();

            transaction.Apply();
            db.Entry(transaction.SavingsFund).State = EntityState.Modified;
            db.Transactions.Add(transaction);
            db.SaveChanges();
            return RedirectToAction("Breakdown", "SavingsAccount", new { id = transaction.SavingsFund.AccountID });
        }

        public ActionResult Withdraw(int fundId)
        {
            ViewBag.Fund = db.SavingsFunds.Find(fundId);

            return View();
        }

        [HttpPost]
        public ActionResult Withdraw([Bind(Include = "Id,Amount,FundId")] Transaction transaction)
        {
            transaction.Type = Transaction.TransactionType.Withdrawal;

            if (!ModelState.IsValid) return View();

            transaction.SavingsFund = db.SavingsFunds.Find(transaction.FundId);
            if (transaction.SavingsFund == null) return View();

            transaction.Apply();
            db.Entry(transaction.SavingsFund).State = EntityState.Modified;
            db.Transactions.Add(transaction);
            db.SaveChanges();
            return RedirectToAction("Breakdown", "SavingsAccount", new { id = transaction.SavingsFund.AccountID });
        }
    }
}