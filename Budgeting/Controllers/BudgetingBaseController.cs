using Budgeting.Models;
using System.Web.Mvc;

namespace Budgeting.Controllers
{
    public class BudgetingBaseController : Controller
    {
        protected IApplicationDbContext db;

        public BudgetingBaseController()
        {
            db = new ApplicationDbContext();
        }

        public BudgetingBaseController(IApplicationDbContext dbContext)
        {
            db = dbContext;
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