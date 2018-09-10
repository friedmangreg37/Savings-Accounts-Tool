using System;
using System.Collections.Generic;
using System.Linq;
using Budgeting.Models;
using NUnit.Framework;

namespace Budgeting.UnitFixtures.Models
{
    [TestFixture]
    public class TestSavingsAccount
    {
        SavingsAccount account;

        [SetUp]
        public void SetUp()
        {
            account = new SavingsAccount();
        }

        [Test]
        public void Test_SumOfFunds_NoFunds_Returns0()
        {
            Assert.AreEqual(0, account.SumOfFunds);
        }

        [Test]
        public void Test_SumOfFunds_OneFund_ReturnsFundBalance()
        {
            var balance = 37.42m;
            account.Funds.Add(new SavingsFund { Balance = balance });

            Assert.AreEqual(balance, account.SumOfFunds);
        }

        [Test]
        public void Test_SumOfFunds_MultipleFunds_ReturnsSumOfBalances()
        {
            var balances = new decimal[] { 45.67m, 37.42m, 6.7m, 1.2m, 132.56m };
            foreach (var balance in balances)
            {
                account.Funds.Add(new SavingsFund { Balance = balance });
            }
            var expected = balances.Sum();

            Assert.AreEqual(expected, account.SumOfFunds);
        }
    }
}
