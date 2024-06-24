using BarbourLogic.Abstractions.Services;
using BarbourLogic.Abstractions.Repository;
using BarbourLogic.Abstractions.Entities;
using BarbourLogic.Abstractions.Services.BarbourLogic.Abstractions.Services;

namespace BarbourLogic.Application
{
    public class AccountManager : IAccountManager
    {
        private readonly IAccountRepository _accountRepository;

        public AccountManager(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void AddAccount(string id, string name)
        {
            // Create an Account object and pass it to the repository for storage
            var account = new Account { Id = id, Name = name, Balance = 0 };
            _accountRepository.AddAccount(account);
        }

        public void DepositMoney(string id, double amount)
        {
            // Implementation to be added
            throw new System.NotImplementedException();
        }

        public void WithdrawMoney(string id, double amount)
        {
            // Implementation to be added
            throw new System.NotImplementedException();
        }

        public Account GetAccountDetails(string id)
        {
            // Implementation to be added
            throw new System.NotImplementedException();
        }
    }
}
