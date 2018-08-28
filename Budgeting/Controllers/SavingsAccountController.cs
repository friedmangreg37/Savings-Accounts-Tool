﻿using Budgeting.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Budgeting.Controllers
{
    public class SavingsAccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            return View(db.SavingsAccounts);
        }

        // GET: SavingsAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SavingsAccount/Create
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(SavingsAccount account)
        {
            if (!ModelState.IsValid) return View(account);

            db.SavingsAccounts.Add(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SavingsAccount/Breakdown/5
        public ActionResult Breakdown(int id)
        {
            var account = db.SavingsAccounts.Find(id);
            if (account == null) return HttpNotFound();

            account.Funds = db.SavingsFunds.Where(f => f.AccountID == account.Id).ToList();
            return View(account);
        }

        // POST: SavingsAccount/Breakdown/5
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Breakdown(SavingsAccount account)
        {
            if(!ModelState.IsValid) return View();

            db.Entry(account).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SavingsAccount/Delete/5
        public ActionResult Delete(int id)
        {
            var account = db.SavingsAccounts.Find(id);
            if (account == null) return HttpNotFound();

            return View(account);
        }

        // POST: SavingsAccount/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var account = db.SavingsAccounts.Find(id);
            db.SavingsAccounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}