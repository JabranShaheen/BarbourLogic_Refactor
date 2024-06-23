using System;
using System.Collections.Generic;

namespace BankingSystem
{
    class Program
    {
        static List<Account> accounts = new List<Account>();

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
            var name = Console.Read().ToString();

            Account account = new Account { Id = id, Name = name, Balance = 0 };
            accounts.Add(account);

            Console.WriteLine("Account added successfully.");
        }

        static void DepositMoney()
        {
            Console.WriteLine("Enter Account ID:");
            string id = Console.ReadLine();

            Console.WriteLine("Enter Amount to Deposit:");
            double amount = double.Parse(Console.ReadLine());

            foreach (var account in accounts)
            {
                if (account.Id == id)
                {
                    account.Balance += amount;
                    Console.WriteLine("Deposit successful.");
                    return;
                }
            }

            Console.WriteLine("Account not found.");
        }

        static void WithdrawMoney()
        {
            Console.WriteLine("Enter Account ID:");
            string id = Console.ReadLine();

            Console.WriteLine("Enter Amount to Withdraw:");
            double amount = double.Parse(Console.ReadLine());

            foreach (var account in accounts)
            {
                if (account.Id == id)
                {
                    if (account.Balance >= amount)
                    {
                        account.Balance -= amount;
                        Console.WriteLine("Withdrawal successful.");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient balance.");
                    }
                    return;
                }
            }

            Console.WriteLine("Account not found.");
        }

        static void DisplayAccountDetails()
        {
            Console.WriteLine("Enter Account ID:");
            string id = Console.ReadLine();

            foreach (var account in accounts)
            {
                if (account.Id == id)
                {
                    Console.WriteLine($"Account ID: {account.Id}");
                    Console.WriteLine($"Account Holder: {account.Name}");
                    Console.WriteLine($"Balance: {account.Balance}");
                    return;
                }
            }

            Console.WriteLine("Account not found.");
        }
    }

    class Account
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
    }
}
