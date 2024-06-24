using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BarbourLogic.Abstractions.Services;
using BarbourLogic.Abstractions.Repository;
using BarbourLogic.Application;
using BarbourLogic.Abstractions.Services.BarbourLogic.Abstractions.Services;
using BarbourLogic.Abstractions.Entities;
using BarbourLogic.Implementations.Services;
using BarbourLogic.Application.Exceptions;

namespace BarbourLogic.Tests
{
    [TestClass]
    public class AccountManagerTests
    {
        [TestMethod]
        public void AddAccount_Should_Add_Account_Successfully()
        {
            // Arrange
            var mockRepository = new Mock<IAccountRepository>();
            IAccountManager accountManager = new AccountManager(mockRepository.Object);

            string accountId = "1";
            string accountName = "John Doe";

            // Act
            accountManager.AddAccount(accountId, accountName);

            // Assert
            mockRepository.Verify(r => r.AddAccount(It.IsAny<Account>()), Times.Once);
            mockRepository.Verify(r => r.AddAccount(It.Is<Account>(a => a.Id == accountId && a.Name == accountName)), Times.Once);
        }

        [TestMethod]
        public void GetAccountDetails_Should_Return_Correct_Account()
        {
            // Arrange
            var mockRepository = new Mock<IAccountRepository>();
            IAccountManager accountManager = new AccountManager(mockRepository.Object);

            string accountId = "1";
            string accountName = "John Doe";
            double initialBalance = 100.0;

            // Mock repository behavior
            mockRepository.Setup(r => r.GetAccountById(accountId))
                          .Returns(new Account { Id = accountId, Name = accountName, Balance = initialBalance });

            // Act
            var account = accountManager.GetAccountDetails(accountId);

            // Assert
            Assert.IsNotNull(account, "Account should not be null.");
            Assert.AreEqual(accountId, account.Id, "Account ID should match.");
            Assert.AreEqual(accountName, account.Name, "Account name should match.");
            Assert.AreEqual(initialBalance, account.Balance, "Account balance should match.");
        }

        [TestMethod]
        public void DepositMoney_Should_Increase_Account_Balance()
        {
            // Arrange
            var mockRepository = new Mock<IAccountRepository>();
            IAccountManager accountManager = new AccountManager(mockRepository.Object);

            string accountId = "1";
            double initialBalance = 100.0;
            double depositAmount = 50.0;

            // Mock repository behavior
            mockRepository.Setup(r => r.GetAccountById(accountId))
                          .Returns(new Account { Id = accountId, Balance = initialBalance });

            // Act
            accountManager.DepositMoney(accountId, depositAmount);

            // Assert
            mockRepository.Verify(r => r.GetAccountById(accountId), Times.Once);
            mockRepository.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Once);

            // Check updated balance
            mockRepository.Verify(r => r.UpdateAccount(It.Is<Account>(a => a.Id == accountId && a.Balance == initialBalance + depositAmount)), Times.Once);
        }

        [TestMethod]
        public void WithdrawMoney_Should_Decrease_Account_Balance()
        {
            // Arrange
            var mockRepository = new Mock<IAccountRepository>();
            IAccountManager accountManager = new AccountManager(mockRepository.Object);

            string accountId = "1";
            double initialBalance = 100.0;
            double withdrawalAmount = 50.0;

            // Mock repository behavior
            mockRepository.Setup(r => r.GetAccountById(accountId))
                          .Returns(new Account { Id = accountId, Balance = initialBalance });

            // Act
            accountManager.WithdrawMoney(accountId, withdrawalAmount);

            // Assert
            mockRepository.Verify(r => r.GetAccountById(accountId), Times.Once);
            mockRepository.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Once);

            // Check updated balance
            mockRepository.Verify(r => r.UpdateAccount(It.Is<Account>(a => a.Id == accountId && a.Balance == initialBalance - withdrawalAmount)), Times.Once);
        }

        [TestMethod]
        public void TransferMoney_Should_Transfer_Funds_Between_Accounts()
        {
            // Arrange
            var mockRepository = new Mock<IAccountRepository>();
            IAccountManager accountManager = new AccountManager(mockRepository.Object);

            string sourceAccountId = "1";
            string destinationAccountId = "2";
            double initialSourceBalance = 100.0;
            double initialDestinationBalance = 50.0;
            double transferAmount = 30.0;

            // Mock repository behavior
            mockRepository.Setup(r => r.GetAccountById(sourceAccountId))
                          .Returns(new Account { Id = sourceAccountId, Balance = initialSourceBalance });

            mockRepository.Setup(r => r.GetAccountById(destinationAccountId))
                          .Returns(new Account { Id = destinationAccountId, Balance = initialDestinationBalance });

            // Act
            accountManager.TransferMoney(sourceAccountId, destinationAccountId, transferAmount);

            // Assert
            mockRepository.Verify(r => r.GetAccountById(sourceAccountId), Times.Once);
            mockRepository.Verify(r => r.GetAccountById(destinationAccountId), Times.Once);
            mockRepository.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Exactly(2)); // Once for source, once for destination

            // Check updated balances
            mockRepository.Verify(r => r.UpdateAccount(It.Is<Account>(a => a.Id == sourceAccountId && a.Balance == initialSourceBalance - transferAmount)), Times.Once);
            mockRepository.Verify(r => r.UpdateAccount(It.Is<Account>(a => a.Id == destinationAccountId && a.Balance == initialDestinationBalance + transferAmount)), Times.Once);
        }

        [TestMethod]
        public void TransferMoney_Should_Throw_AccountNotFoundException_For_Invalid_Source_Account()
        {
            // Arrange
            var mockRepository = new Mock<IAccountRepository>();
            IAccountManager accountManager = new AccountManager(mockRepository.Object);

            string sourceAccountId = "1";
            string destinationAccountId = "2";
            double transferAmount = 30.0;

            // Mock repository behavior (only mock for destination account)
            mockRepository.Setup(r => r.GetAccountById(destinationAccountId))
                          .Returns(new Account { Id = destinationAccountId });

            // Act and Assert
            try
            {
                accountManager.TransferMoney(sourceAccountId, destinationAccountId, transferAmount);
                Assert.Fail("Expected AccountNotFoundException was not thrown.");
            }
            catch (AccountNotFoundException ex)
            {
                Assert.AreEqual($"Source account with ID '{sourceAccountId}' not found.", ex.Message);
            }
        }

        [TestMethod]
        public void TransferMoney_Should_Throw_AccountNotFoundException_For_Invalid_Destination_Account()
        {
            // Arrange
            var mockRepository = new Mock<IAccountRepository>();
            IAccountManager accountManager = new AccountManager(mockRepository.Object);

            string sourceAccountId = "1";
            string destinationAccountId = "2";
            double transferAmount = 30.0;

            // Mock repository behavior (only mock for source account)
            mockRepository.Setup(r => r.GetAccountById(sourceAccountId))
                          .Returns(new Account { Id = sourceAccountId });

            // Act and Assert
            try
            {
                accountManager.TransferMoney(sourceAccountId, destinationAccountId, transferAmount);
                Assert.Fail("Expected AccountNotFoundException was not thrown.");
            }
            catch (AccountNotFoundException ex)
            {
                Assert.AreEqual($"Destination account with ID '{destinationAccountId}' not found.", ex.Message);
            }
        }

        [TestMethod]
        public void TransferMoney_Should_Throw_InsufficientBalanceException_If_Source_Account_Has_Insufficient_Balance()
        {
            // Arrange
            var mockRepository = new Mock<IAccountRepository>();
            IAccountManager accountManager = new AccountManager(mockRepository.Object);

            string sourceAccountId = "1";
            string destinationAccountId = "2";
            double initialSourceBalance = 20.0; // Less than transfer amount
            double transferAmount = 30.0;

            // Mock repository behavior
            mockRepository.Setup(r => r.GetAccountById(sourceAccountId))
                          .Returns(new Account { Id = sourceAccountId, Balance = initialSourceBalance });

            mockRepository.Setup(r => r.GetAccountById(destinationAccountId))
                          .Returns(new Account { Id = destinationAccountId });

            // Act and Assert
            try
            {
                accountManager.TransferMoney(sourceAccountId, destinationAccountId, transferAmount);
                Assert.Fail("Expected InsufficientBalanceException was not thrown.");
            }
            catch (InsufficientBalanceException ex)
            {
                Assert.AreEqual($"Insufficient balance in source account with ID '{sourceAccountId}'.", ex.Message);
            }
        }
    }
}
