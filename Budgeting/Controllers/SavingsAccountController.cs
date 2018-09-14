using Budgeting.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Budgeting.Controllers
{
    [Authorize]
    public class SavingsAccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var userAccounts = db.SavingsAccounts.Where(s => s.ApplicationUserId == userId).ToList();
            foreach (var account in userAccounts)
            {
                account.Funds = db.SavingsFunds.Where(f => f.AccountID == account.Id).ToList();
            }
            return View(userAccounts);
        }

        public ActionResult Create()
        {
            SavingsAccount account = new SavingsAccount
            {
                ApplicationUserId = User.Identity.GetUserId()
            };
            return View(account);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ApplicationUserId")] SavingsAccount account)
        {
            if (!ModelState.IsValid) return View(account);

            db.SavingsAccounts.Add(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Breakdown(int id)
        {
            var account = db.SavingsAccounts.Find(id);
            if (account == null) return HttpNotFound();

            account.Funds = db.SavingsFunds.Where(f => f.AccountID == account.Id).ToList();
            return View(account);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Breakdown(SavingsAccount account)
        {
            if(!ModelState.IsValid) return View(account);

            db.Entry(account).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var account = db.SavingsAccounts.Find(id);
            if (account == null) return HttpNotFound();

            return View(account);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var account = db.SavingsAccounts.Find(id);
            db.SavingsAccounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
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
