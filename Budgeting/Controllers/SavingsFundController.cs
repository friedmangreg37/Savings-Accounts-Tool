using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Budgeting.Models;

namespace Budgeting.Controllers
{
    [Authorize]
    public class SavingsFundController : Controller
    {
        private IApplicationDbContext db;

        public SavingsFundController()
        {
            db = new ApplicationDbContext();
        }

        public SavingsFundController(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public ActionResult Create(int accountID)
        {
            if (db.SavingsAccounts.Find(accountID) == null) return HttpNotFound();

            var fund = new SavingsFund {AccountID = accountID};
            return View(fund);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,AmountPerMonth,Balance,AccountID")] SavingsFund fund)
        {
            if (!ModelState.IsValid) return View(fund);

            db.SavingsFunds.Add(fund);
            db.SaveChanges();
            return RedirectToAction("Breakdown", "SavingsAccount", new {id = fund.AccountID});
        }

        public ActionResult Edit(int id)
        {
            var fund = db.SavingsFunds.Find(id);
            if (fund == null) return HttpNotFound();

            return View(fund);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(SavingsFund fund)
        {
            if (!ModelState.IsValid) return View(fund);

            db.SetEntityState(fund, EntityState.Modified);
            db.SaveChanges();
            return RedirectToAction("Breakdown", "SavingsAccount", new {id = fund.AccountID});
        }

        public ActionResult Withdraw(int? id, decimal amount)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var fund = db.SavingsFunds.Find(id);
            if (fund == null) return HttpNotFound();

            fund.Withdraw(amount);
            db.SetEntityState(fund, EntityState.Modified);
            db.SaveChanges();
            return RedirectToAction("Breakdown", "SavingsAccount", new { id = fund.AccountID });
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var fund = db.SavingsFunds.Find(id);
            if (fund == null) return HttpNotFound();

            return View(fund);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var fund = db.SavingsFunds.Find(id);
            db.SavingsFunds.Remove(fund);
            db.SaveChanges();
            return RedirectToAction("Breakdown", "SavingsAccount", new {id = fund.AccountID});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
