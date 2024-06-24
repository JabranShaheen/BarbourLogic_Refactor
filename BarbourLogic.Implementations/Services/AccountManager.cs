using BarbourLogic.Abstractions.Entities;
using BarbourLogic.Abstractions.Repository;
using BarbourLogic.Abstractions.Services;
using BarbourLogic.Abstractions.Services.BarbourLogic.Abstractions.Services;
using BarbourLogic.Application.Exceptions;
using System;

namespace BarbourLogic.Implementations.Services
{
    public class AccountManager : IAccountManager
    {
        private readonly IAccountRepository _accountRepository;

        public AccountManager(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public void AddAccount(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Account ID cannot be null or empty.", nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Account holder name cannot be null or empty.", nameof(name));

            // Check if account with same ID already exists
            if (_accountRepository.GetAccountById(id) != null)
                throw new ArgumentException($"An account with ID '{id}' already exists.");

            var account = new Account { Id = id, Name = name, Balance = 0 };
            _accountRepository.AddAccount(account);
        }

        public void DepositMoney(string id, double amount)
        {
            var account = _accountRepository.GetAccountById(id);
            if (account != null)
            {
                account.Balance += amount;
                _accountRepository.UpdateAccount(account);
            }
            else
            {
                throw new AccountNotFoundException($"Account with ID '{id}' not found.");
            }
        }

        public void WithdrawMoney(string id, double amount)
        {
            var account = _accountRepository.GetAccountById(id);
            if (account != null)
            {
                if (account.Balance >= amount)
                {
                    account.Balance -= amount;
                    _accountRepository.UpdateAccount(account);
                }
                else
                {
                    throw new InsufficientBalanceException($"Insufficient balance in account with ID '{id}'.");
                }
            }
            else
            {
                throw new AccountNotFoundException($"Account with ID '{id}' not found.");
            }
        }

        public void TransferMoney(string sourceId, string destinationId, double amount)
        {
            if (string.IsNullOrWhiteSpace(sourceId))
                throw new ArgumentException("Source account ID cannot be null or empty.", nameof(sourceId));

            if (string.IsNullOrWhiteSpace(destinationId))
                throw new ArgumentException("Destination account ID cannot be null or empty.", nameof(destinationId));

            if (amount <= 0)
                throw new ArgumentException("Transfer amount must be greater than zero.", nameof(amount));

            var sourceAccount = _accountRepository.GetAccountById(sourceId);
            var destinationAccount = _accountRepository.GetAccountById(destinationId);

            if (sourceAccount == null)
                throw new AccountNotFoundException($"Source account with ID '{sourceId}' not found.");

            if (destinationAccount == null)
                throw new AccountNotFoundException($"Destination account with ID '{destinationId}' not found.");

            if (sourceAccount.Balance < amount)
                throw new InsufficientBalanceException($"Insufficient balance in source account with ID '{sourceId}'.");

            sourceAccount.Balance -= amount;
            destinationAccount.Balance += amount;

            _accountRepository.UpdateAccount(sourceAccount);
            _accountRepository.UpdateAccount(destinationAccount);
        }

        public Account GetAccountDetails(string id)
        {
            var account = _accountRepository.GetAccountById(id);
            if (account == null)
            {
                throw new AccountNotFoundException($"Account with ID '{id}' not found.");
            }
            return account;
        }
    }
}
