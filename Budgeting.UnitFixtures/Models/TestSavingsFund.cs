using Budgeting.Models;
using NUnit.Framework;

namespace Budgeting.UnitFixtures.Models
{
    [TestFixture]
    public class TestSavingsFund
    {
        SavingsFund fund;

        [SetUp]
        public void SetUp()
        {
            fund = new SavingsFund();
        }

        [Test]
        public void Test_Deposit_AmountSavedIncreasesAccordingly()
        {
            fund.Balance = 45.67m;
            var deposit = 44.56m;
            var expected = fund.Balance + deposit;

            fund.Deposit(deposit);

            Assert.AreEqual(expected, fund.Balance);
        }

        [Test]
        public void Test_Withdraw_AmountSavedDecreasesAccordingly()
        {
            fund.Balance = 45.67m;
            var withdrawal = 32.56m;
            var expected = fund.Balance - withdrawal;

            fund.Withdraw(withdrawal);

            Assert.AreEqual(expected, fund.Balance);
        }
    }
}
