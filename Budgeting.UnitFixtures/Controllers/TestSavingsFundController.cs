using Budgeting.Controllers;
using Budgeting.Models;
using Moq;
using NUnit.Framework;
using System.Data.Entity;
using System.Web.Mvc;

namespace Budgeting.UnitFixtures.Controllers
{
    [TestFixture]
    public class TestSavingsFundController
    {
        private SavingsFundController controller;
        private Mock<IApplicationDbContext> mockDbContext;
        private Mock<IDbSet<SavingsAccount>> mockAccounts;
        private Mock<IDbSet<SavingsFund>> mockFunds;

        [SetUp]
        public void SetUp()
        {
            mockAccounts = new Mock<IDbSet<SavingsAccount>>();
            mockFunds = new Mock<IDbSet<SavingsFund>>();
            mockDbContext = new Mock<IApplicationDbContext>();
            mockDbContext.Setup(c => c.SavingsAccounts).Returns(mockAccounts.Object);
            mockDbContext.Setup(c => c.SavingsFunds).Returns(mockFunds.Object);
            controller = new SavingsFundController(mockDbContext.Object);
        }

        [Test]
        public void Test_CreateGet_InvalidAccountId_ReturnsHttpNotFound()
        {
            mockAccounts.Setup(a => a.Find(It.IsAny<int>())).Returns((SavingsAccount)null);

            var result = controller.Create(0);

            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
        }

        [Test]
        public void Test_CreateGet_ValidAccountId_ReturnsViewWithFund()
        {
            var account = new SavingsAccount { Id = 1 };
            mockAccounts.Setup(a => a.Find(account.Id)).Returns(account);

            var result = controller.Create(account.Id) as ViewResult;

            Assert.IsNotNull(result);
            var fund = result.Model as SavingsFund;
            Assert.IsNotNull(fund);
            Assert.AreEqual(1, fund.AccountID);
        }

        [Test]
        public void Test_CreatePost_InvalidModelState_ReturnsViewWithFund()
        {
            controller.ModelState.AddModelError("test", "test");
            var fund = new SavingsFund();

            var result = controller.Create(fund) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(fund, result.Model as SavingsFund);
        }

        [Test]
        public void Test_CreatePost_ValidModelState_SavesToDB()
        {
            controller.ModelState.Clear();
            var fund = new SavingsFund {AccountID = 1, AmountPerMonth = 50, Balance = 50, Name = "Test Fund"};

            var result = controller.Create(fund) as RedirectToRouteResult;

            mockDbContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], "Breakdown");
            Assert.AreEqual(result.RouteValues["controller"], "SavingsAccount");
            Assert.AreEqual(result.RouteValues["id"], fund.AccountID);
        }

        [Test]
        public void Test_EditGet_InvalidId_ReturnsHttpNotFound()
        {
            mockFunds.Setup(a => a.Find(It.IsAny<int>())).Returns((SavingsFund)null);

            var result = controller.Edit(0);

            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
        }

        [Test]
        public void Test_EditGet_ValidId_ReturnsViewWithFund()
        {
            var fund = new SavingsFund {Id = 7};
            mockFunds.Setup(a => a.Find(It.IsAny<int>())).Returns(fund);

            var result = controller.Edit(fund.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(fund, result.Model as SavingsFund);
        }

        [Test]
        public void Test_EditPost_InvalidModelState_ReturnsViewWithFund()
        {
            controller.ModelState.AddModelError("test", "test");
            var fund = new SavingsFund();

            var result = controller.Edit(fund) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(fund, result.Model as SavingsFund);
        }

        [Test]
        public void Test_EditPost_ValidModelState_SavesToDB()
        {
            controller.ModelState.Clear();
            var fund = new SavingsFund { AccountID = 1, AmountPerMonth = 50, Balance = 50, Name = "Test Fund" };

            var result = controller.Edit(fund) as RedirectToRouteResult;

            mockDbContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], "Breakdown");
            Assert.AreEqual(result.RouteValues["controller"], "SavingsAccount");
            Assert.AreEqual(result.RouteValues["id"], fund.AccountID);
        }

        [Test]
        public void Test_Delete_InvalidId_ReturnsHttpNotFound()
        {
            mockFunds.Setup(a => a.Find(It.IsAny<int>())).Returns((SavingsFund)null);

            var result = controller.Delete(0);

            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
        }

        [Test]
        public void Test_Delete_ValidId_ReturnsViewWithFund()
        {
            var fund = new SavingsFund { Id = 7 };
            mockFunds.Setup(a => a.Find(It.IsAny<int>())).Returns(fund);

            var result = controller.Delete(fund.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(fund, result.Model as SavingsFund);
        }

        [Test]
        public void Test_DeleteConfirmed_RemovesFundAndSaves()
        {
            var fund = new SavingsFund { Id = 7 };
            mockFunds.Setup(a => a.Find(It.IsAny<int>())).Returns(fund);

            var result = controller.DeleteConfirmed(fund.Id) as RedirectToRouteResult;

            mockFunds.Verify(f => f.Remove(fund), Times.Once);
            mockDbContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], "Breakdown");
            Assert.AreEqual(result.RouteValues["controller"], "SavingsAccount");
            Assert.AreEqual(result.RouteValues["id"], fund.AccountID);
        }
    }
}
