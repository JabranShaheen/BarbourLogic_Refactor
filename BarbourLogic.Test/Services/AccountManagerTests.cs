using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BarbourLogic.Abstractions.Services;
using BarbourLogic.Abstractions.Repository;
using BarbourLogic.Application;
using BarbourLogic.Abstractions.Services.BarbourLogic.Abstractions.Services;
using BarbourLogic.Abstractions.Entities;

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
    }
}
