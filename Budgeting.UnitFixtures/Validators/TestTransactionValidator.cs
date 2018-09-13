using Budgeting.Models;
using Budgeting.Validators;
using NUnit.Framework;

namespace Budgeting.UnitFixtures.Validators
{
    [TestFixture]
    public class TestTransactionValidator
    {
        [Test]
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

        [Test]
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

        [Test]
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
            Assert.Contains(TransactionValidator.INSUFFICIENT_FUNDS_ERROR_MESSAGE, validator.ErrorMessages);
        }

        [Test]
        public void Test_Validate_NoFundAssociated_ResultsInError()
        {
            var transaction = new Transaction();
            var validator = new TransactionValidator();

            validator.Validate(transaction);

            Assert.IsTrue(validator.HasErrors);
            Assert.Contains(TransactionValidator.NO_SAVINGS_FUND_ERROR_MESSAGE, validator.ErrorMessages);
        }
    }
}
