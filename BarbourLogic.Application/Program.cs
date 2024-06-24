using System;
using BarbourLogic.Abstractions.Repository;
using BarbourLogic.Application;
using BarbourLogic.Abstractions.Entities;
using BarbourLogic.Abstractions.Services.BarbourLogic.Abstractions.Services;
using BarbourLogic.Application.Exceptions;
using BarbourLogic.Implementations.Services;
using BarbourLogic.Implementations;

namespace BankingSystem
{
    class Program
    {
        // Injected AccountManager instance
        private static readonly IAccountManager accountManager = new AccountManager(new AccountRepository());

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Add Account");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. Display Account Details");
                Console.WriteLine("5. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddAccount();
                        break;
                    case "2":
                        DepositMoney();
                        break;
                    case "3":
                        WithdrawMoney();
                        break;
                    case "4":
                        DisplayAccountDetails();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void AddAccount()
        {
            Console.WriteLine("Enter Account ID:");
            var id = Console.ReadLine();

            Console.WriteLine("Enter Account Holder Name:");
            var name = Console.ReadLine();

            accountManager.AddAccount(id, name);

            Console.WriteLine("Account added successfully.");
        }

        static void DepositMoney()
        {
            Console.WriteLine("Enter Account ID:");
            string id = Console.ReadLine();

            Console.WriteLine("Enter Amount to Deposit:");
            double amount = double.Parse(Console.ReadLine());

            try
            {
                accountManager.DepositMoney(id, amount);
                Console.WriteLine("Deposit successful.");
            }
            catch (AccountNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void WithdrawMoney()
        {
            Console.WriteLine("Enter Account ID:");
            string id = Console.ReadLine();

            Console.WriteLine("Enter Amount to Withdraw:");
            double amount = double.Parse(Console.ReadLine());

            try
            {
                accountManager.WithdrawMoney(id, amount);
                Console.WriteLine("Withdrawal successful.");
            }
            catch (AccountNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void DisplayAccountDetails()
        {
            Console.WriteLine("Enter Account ID:");
            string id = Console.ReadLine();

            try
            {
                var account = accountManager.GetAccountDetails(id);
                Console.WriteLine($"Account ID: {account.Id}");
                Console.WriteLine($"Account Holder: {account.Name}");
                Console.WriteLine($"Balance: {account.Balance}");
            }
            catch (AccountNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
