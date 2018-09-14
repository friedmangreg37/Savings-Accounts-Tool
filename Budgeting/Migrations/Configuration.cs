namespace Budgeting.Migrations
{
    using Budgeting.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Budgeting.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //var userStore = new UserStore<ApplicationUser>(context);
            //var userManager = new UserManager<ApplicationUser>(userStore);

            //if (!context.Users.Any(t => t.UserName == "admin"))
            //{
            //    var user = new ApplicationUser {UserName = "admin", Email = "admin@email.com"};
            //    userManager.Create(user, "Password1!");

            //    context.Roles.AddOrUpdate(r => r.Name, new IdentityRole {Name = "Admin"});
            //    context.SaveChanges();

            //    userManager.AddToRole(user.Id, "Admin");
            //}
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
