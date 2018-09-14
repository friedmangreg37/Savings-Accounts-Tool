using System.Data.Entity;
using Budgeting.Controllers;
using Budgeting.Models;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Budgeting.UnitFixtures.Controllers
{
    [TestFixture]
    public class TestSavingsAccountController
    {
        private Mock<SavingsAccountController> mockController;
        private Mock<IApplicationDbContext> mockDbContext;
        private Mock<IDbSet<SavingsAccount>> mockAccounts;
        private string userId;

        [SetUp]
        public void SetUp()
        {
            userId = "Test ID";
            mockAccounts = new Mock<IDbSet<SavingsAccount>>();
            mockDbContext = new Mock<IApplicationDbContext>();
            mockController = new Mock<SavingsAccountController>(mockDbContext.Object);

            mockDbContext.Setup(c => c.SavingsAccounts).Returns(mockAccounts.Object);
            mockController.Setup(c => c.GetUserId()).Returns(userId);
            mockController.CallBase = true;
        }

        [Test]
        public void Test_Index_ReturnsViewWithAccountList()
        {
            var testAccounts = new List<SavingsAccount>
            {
                new SavingsAccount()
            };
            mockController.Setup(c => c.GetAccountsByUser(It.IsAny<string>())).Returns(testAccounts);

            var result = mockController.Object.Index() as ViewResult;

            mockController.Verify(c => c.GetAccountsByUser(userId), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(testAccounts, result.Model);
        }

        [Test]
        public void Test_CreateGet_ReturnsViewWithAccount()
        {
            var result = mockController.Object.Create() as ViewResult;

            Assert.IsNotNull(result);
            var modelAccount = result.Model as SavingsAccount;
            Assert.IsNotNull(modelAccount);
            Assert.AreEqual(userId, modelAccount.ApplicationUserId);
        }

        [Test]
        public void Test_CreatePost_InvalidModelState_ReturnsViewWithAccount()
        {
            mockController.Object.ModelState.AddModelError("test", "test");
            var account = new SavingsAccount();

            var result = mockController.Object.Create(account) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(account, result.Model);
        }

        [Test]
        public void Test_CreatePost_ValidModelState_AddsAndSaves()
        {
            mockController.Object.ModelState.Clear();
            var account = new SavingsAccount {Id = 2, Name = "Test", ApplicationUserId = userId};

            var result = mockController.Object.Create(account) as RedirectToRouteResult;

            mockAccounts.Verify(a => a.Add(account), Times.Once);
            mockDbContext.Verify(c => c.SaveChanges(), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [Test]
        public void Test_BreakdownGet_InvalidId_ReturnsHttpNotFound()
        {
            mockAccounts.Setup(a => a.Find(It.IsAny<int>())).Returns((SavingsAccount) null);

            var result = mockController.Object.Breakdown(0);

            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
        }

        [Test]
        public void Test_BreakdownGet_ValidId_ReturnsViewWithAccount()
        {
            var account = new SavingsAccount { Id = 7 };
            mockAccounts.Setup(a => a.Find(account.Id)).Returns(account);
            mockController.Setup(c => c.LoadSavingsFunds(It.IsAny<SavingsAccount>()));

            var result = mockController.Object.Breakdown(account.Id) as ViewResult;

            mockController.Verify(c => c.LoadSavingsFunds(account), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(account, result.Model);
        }

        [Test]
        public void Test_BreakdownPost_InvalidModelState_ReturnsViewWithAccount()
        {
            mockController.Object.ModelState.AddModelError("test", "test");
            var account = new SavingsAccount();

            var result = mockController.Object.Breakdown(account) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(account, result.Model);
        }

        [Test]
        public void Test_BreakdownPost_ValidModelState_SavesToDB()
        {
            mockController.Object.ModelState.Clear();
            var account = new SavingsAccount { Id = 2, Name = "Test", ApplicationUserId = userId };

            var result = mockController.Object.Breakdown(account) as RedirectToRouteResult;

            mockDbContext.Verify(c => c.SaveChanges(), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [Test]
        public void Test_Delete_InvalidId_ReturnsHttpNotFound()
        {
            mockAccounts.Setup(a => a.Find(It.IsAny<int>())).Returns((SavingsAccount)null);

            var result = mockController.Object.Delete(0);

            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
        }

        [Test]
        public void Test_Delete_ValidId_ReturnsViewWithAccount()
        {
            var account = new SavingsAccount { Id = 7 };
            mockAccounts.Setup(a => a.Find(It.IsAny<int>())).Returns(account);

            var result = mockController.Object.Delete(account.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(account, result.Model);
        }

        [Test]
        public void Test_DeleteConfirmed_RemovesAccountAndSaves()
        {
            var account = new SavingsAccount { Id = 7 };
            mockAccounts.Setup(a => a.Find(It.IsAny<int>())).Returns(account);

            var result = mockController.Object.DeleteConfirmed(account.Id) as RedirectToRouteResult;

            mockAccounts.Verify(f => f.Remove(account), Times.Once);
            mockDbContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }
    }
}
