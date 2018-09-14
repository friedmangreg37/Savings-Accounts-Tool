using Budgeting.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Budgeting.Controllers
{
    [Authorize]
    public class SavingsAccountController : Controller
    {
        private IApplicationDbContext db;

        public SavingsAccountController()
        {
            db = new ApplicationDbContext();
        }

        public SavingsAccountController(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public ActionResult Index()
        {
            var userAccounts = GetAccountsByUser(GetUserId());

            return View(userAccounts);
        }

        public ActionResult Create()
        {
            SavingsAccount account = new SavingsAccount
            {
                ApplicationUserId = GetUserId()
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

            LoadSavingsFunds(account);
            return View(account);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Breakdown(SavingsAccount account)
        {
            if(!ModelState.IsValid) return View(account);

            db.SetEntityState(account, EntityState.Modified);
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

        public virtual string GetUserId()
        {
            return User.Identity.GetUserId();
        }

        public virtual List<SavingsAccount> GetAccountsByUser(string userId)
        {
            // maybe wanna pull this out into a service class or something
            var userAccounts = db.SavingsAccounts.Where(s => s.ApplicationUserId == userId).ToList();
            foreach (var account in userAccounts)
            {
                LoadSavingsFunds(account);
            }
            return userAccounts;
        }

        public virtual void LoadSavingsFunds(SavingsAccount account)
        {
            account.Funds = db.SavingsFunds.Where(f => f.AccountID == account.Id).ToList();
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
