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
    }
}
