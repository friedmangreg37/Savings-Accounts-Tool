using Budgeting.Models;
using System.Data.Entity;
using System.Web.Mvc;

namespace Budgeting.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Create(int fundId, Transaction.TransactionType type)
        {
            ViewBag.Fund = db.SavingsFunds.Find(fundId);
            ViewBag.TransactionType = type.ToString();

            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Amount,FundId,Type")] Transaction transaction)
        {
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