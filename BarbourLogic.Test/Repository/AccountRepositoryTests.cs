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

        [TestMethod]
        public void UpdateAccount_Should_Update_Account_Successfully()
        {
            // Arrange
            IAccountRepository repository = new AccountRepository();
            var account = new Account { Id = "1", Name = "John Doe", Balance = 100.0 };
            repository.AddAccount(account);

            // Act
            account.Name = "Jane Doe"; // Update the account name
            repository.UpdateAccount(account);

            // Assert
            var updatedAccount = repository.GetAccountById("1");
            Assert.IsNotNull(updatedAccount, "Account should not be null after update.");
            Assert.AreEqual("Jane Doe", updatedAccount.Name, "Account name should be updated.");
            Assert.AreEqual(100.0, updatedAccount.Balance, "Account balance should remain unchanged.");
        }
    }
}
