using Budgeting.Models;
using Budgeting.Validators;
using Moq;
using NUnit.Framework;

namespace Budgeting.UnitFixtures.Models
{
    [TestFixture]
    public class TestTransaction
    {
        private Mock<SavingsFund> fundMock;
        private Mock<TransactionValidator> validatorMock;
        private Mock<Transaction> transactionMock;

        [SetUp]
        public void SetUp()
        {
            fundMock = new Mock<SavingsFund>();
            validatorMock = new Mock<TransactionValidator>();
            transactionMock = new Mock<Transaction>();
            transactionMock.SetupGet(t => t.SavingsFund).Returns(fundMock.Object);
            transactionMock.Setup(t => t.GetTransactionValidator()).Returns(validatorMock.Object);
        }

        [Test]
        public void Test_Apply_Deposit_CallsSavingsFundDeposit()
        {
            var amount = 50m;
            transactionMock.Object.Type = Transaction.TransactionType.Deposit;
            transactionMock.Object.Amount = amount;

            transactionMock.Object.Apply();

            fundMock.Verify(f => f.Deposit(amount), Times.Once());
        }

        [Test]
        public void Test_Apply_Withdrawal_CallsSavingsFundWithdraw()
        {
            fundMock.Object.Balance = 100m;
            var amount = 50m;
            transactionMock.Object.Type = Transaction.TransactionType.Withdraw;
            transactionMock.Object.Amount = amount;

            transactionMock.Object.Apply();

            fundMock.Verify(f => f.Withdraw(amount), Times.Once());
        }

        [Test]
        public void Test_Apply_CallsValidator()
        {
            transactionMock.Object.Apply();

            validatorMock.Verify(v => v.Validate(transactionMock.Object), Times.Once());
        }
    }
}
