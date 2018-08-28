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

        [Test]
        public void Test_UnallocatedFunds_NoFunds_Returns0()
        {
            Assert.AreEqual(0, account.UnallocatedFunds);
        }

        [Test]
        public void Test_UnallocatedFunds_FundsLessThanBalance_ReturnsDifference()
        {
            var balances = new decimal[] { 45.67m, 37.42m, 6.7m, 1.2m, 132.56m };
            foreach (var balance in balances)
            {
                account.Funds.Add(new SavingsFund { Balance = balance });
            }
            var difference = 150m;
            account.Balance = balances.Sum() + difference;

            Assert.AreEqual(difference, account.UnallocatedFunds);
        }

        [Test]
        public void Test_UnallocatedFunds_FundsAddToBalance_Returns0()
        {
            var balances = new decimal[] { 45.67m, 37.42m, 6.7m, 1.2m, 132.56m };
            foreach (var balance in balances)
            {
                account.Funds.Add(new SavingsFund { Balance = balance });
            }
            account.Balance = balances.Sum();

            Assert.AreEqual(0, account.UnallocatedFunds);
        }

        [Test]
        public void Test_CreateNewFund_BalanceLessThanUnallocated_AddsFundToAccount()
        {
            account.Balance = 500m;
            var fund = new SavingsFund { Name = "Test", Balance = 250m };

            account.CreateNewFund(fund);

            Assert.AreEqual(1, account.Funds.Count);
            Assert.AreEqual(fund, account.Funds.ElementAt(0));
        }

        [Test]
        public void Test_CreateNewFund_BalanceEqualToUnallocated_AddsFundToAccount()
        {
            account.Balance = 500m;
            var fund = new SavingsFund { Name = "Test", Balance = account.Balance };

            account.CreateNewFund(fund);

            Assert.AreEqual(1, account.Funds.Count);
            Assert.AreEqual(fund, account.Funds.ElementAt(0));
        }

        [Test]
        public void Test_CreateNewFund_BalanceGreaterThanUnallocated_ThrowsException()
        {
            var fund = new SavingsFund { Name = "Test", Balance = 500m };

            void CheckFunction()
            {
                account.CreateNewFund(fund);
            }

            Assert.Throws<ArgumentException>(CheckFunction);
            Assert.AreEqual(0, account.Funds.Count);
        }
    }
}
