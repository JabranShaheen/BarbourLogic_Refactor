using Microsoft.VisualStudio.TestTools.UnitTesting;
using BarbourLogic.Abstractions.Repository;
using BarbourLogic.Implementations;
using BarbourLogic.Abstractions.Entities;

namespace BarbourLogic.Tests
{
    [TestClass]
    public class AccountRepositoryTests
    {
        [TestMethod]
        public void AddAccount_Should_Add_Account_Successfully()
        {
            // Arrange
            IAccountRepository repository = new AccountRepository();
            var account = new Account { Id = "1", Name = "John Doe", Balance = 100.0 };

            // Act
            repository.AddAccount(account);

            // Assert
            var addedAccount = repository.GetAccountById("1");
            Assert.IsNotNull(addedAccount, "Account should not be null after adding.");
            Assert.AreEqual("John Doe", addedAccount.Name, "Account name should match.");
            Assert.AreEqual(100.0, addedAccount.Balance, "Account balance should match.");
        }
    }
}
