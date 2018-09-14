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
        public void Test_Create_InvalidAccountId_ReturnsHttpNotFound()
        {
            mockAccounts.Setup(a => a.Find(It.IsAny<int>())).Returns((SavingsAccount)null);

            var result = controller.Create(0);

            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
        }

        [Test]
        public void Test_Create_ValidAccountId_ReturnsViewWithFund()
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
        public void Test_Create_ValidFund_SavesToDB()
        {
            var fund = new SavingsFund {AccountID = 1, AmountPerMonth = 50, Balance = 50, Name = "Test Fund"};

            var result = controller.Create(fund) as RedirectToRouteResult;

            //Assert.IsTrue(SaveChangesWasCalled);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Test_EditGet_InvalidId_ReturnsHttpNotFound()
        {
            
        }

        [Test]
        public void Test_EditGet_ValidId_ReturnsViewWithSavingsFund()
        {

        }

        [Test]
        public void Test_EditPost_InvalidModelState_ReturnsSameViewWithSavingsFund()
        {

        }

        [Test]
        public void Test_EditPost_ValidModelState_SavesToDB()
        {

        }
    }
}
