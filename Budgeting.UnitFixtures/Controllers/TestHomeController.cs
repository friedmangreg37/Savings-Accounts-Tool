using System.Web.Mvc;
using NUnit.Framework;
using Budgeting.Controllers;

namespace Budgeting.UnitFixtures.Controllers
{
    [TestFixture]
    public class TestHomeController
    {
        [Test]
        public void Index()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void About()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.About() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [Test]
        public void Contact()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Contact() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Your contact page.", result.ViewBag.Message);
        }
    }
}
