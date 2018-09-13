using Budgeting.Models;
using Budgeting.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Budgeting.UnitFixtures.Validators
{
    [TestClass]
    public class TestTransactionValidator
    {
        [TestMethod]
        public void Test_Validate_NormalDeposit_NoErrors()
        {
            var fund = new SavingsFund { Balance = 50 };
            var transaction = new Transaction
            {
                SavingsFund = fund,
                Type = Transaction.TransactionType.Deposit,
                Amount = 50
            };
            var validator = new TransactionValidator();

            validator.Validate(transaction);

            Assert.IsFalse(validator.HasErrors);
        }

        [TestMethod]
        public void Test_Validate_NormalWithdrawal_NoErrors()
        {
            var fund = new SavingsFund { Balance = 50 };
            var transaction = new Transaction
            {
                SavingsFund = fund,
                Type = Transaction.TransactionType.Withdraw,
                Amount = 50
            };
            var validator = new TransactionValidator();

            validator.Validate(transaction);

            Assert.IsFalse(validator.HasErrors);
        }

        [TestMethod]
        public void Test_Validate_WithdrawMoreThanBalance_ResultsInError()
        {
            var fund = new SavingsFund {Balance = 50};
            var transaction = new Transaction
            {
                SavingsFund = fund,
                Type = Transaction.TransactionType.Withdraw,
                Amount = 100
            };
            var validator = new TransactionValidator();

            validator.Validate(transaction);

            Assert.IsTrue(validator.HasErrors);
            CollectionAssert.Contains(validator.ErrorMessages, TransactionValidator.INSUFFICIENT_FUNDS_ERROR_MESSAGE);
        }

        [TestMethod]
        public void Test_Validate_NoFundAssociated_ResultsInError()
        {
            var transaction = new Transaction();
            var validator = new TransactionValidator();

            validator.Validate(transaction);

            Assert.IsTrue(validator.HasErrors);
            CollectionAssert.Contains(validator.ErrorMessages, TransactionValidator.NO_SAVINGS_FUND_ERROR_MESSAGE);
        }
    }
}
