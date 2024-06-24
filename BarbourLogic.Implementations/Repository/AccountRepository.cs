using System;
using System.Collections.Generic;
using BarbourLogic.Abstractions.Entities;
using BarbourLogic.Abstractions.Repository;

namespace BarbourLogic.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        // Simulating in-memory storage for accounts
        private readonly Dictionary<string, Account> _accounts;

        public AccountRepository()
        {
            _accounts = new Dictionary<string, Account>();
        }

        public void AddAccount(Account account)
        {

            _accounts.Add(account.Id, account);
        }

        public Account GetAccountById(string id)
        {
            _accounts.TryGetValue(id, out Account account);
            return account;
        }

        public void UpdateAccount(Account account)
        {
            // TODO: Implement logic to update an account in a data store.
            throw new System.NotImplementedException();
        }
    }
}
