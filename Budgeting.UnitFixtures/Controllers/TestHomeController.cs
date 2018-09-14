using System.Web.Mvc;
using NUnit.Framework;
using Budgeting.Controllers;

namespace Budgeting.UnitFixtures.Controllers
{
    [TestFixture]
    public class TestHomeController
    {
        [Test]
        public void Test_Index_ReturnsViewResult()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void Test_About_ReturnsViewResult()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.About() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [Test]
        public void Test_Contact_ReturnsViewResult()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Contact() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Your contact page.", result.ViewBag.Message);
        }
    }
}
