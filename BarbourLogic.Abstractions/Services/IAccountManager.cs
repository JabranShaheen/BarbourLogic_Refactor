using BarbourLogic.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarbourLogic.Abstractions.Services
{
    namespace BarbourLogic.Abstractions.Services
    {
        /// <summary>
        /// Interface for managing account operations.
        /// </summary>
        public interface IAccountManager
        {
            /// <summary>
            /// Adds a new account with the specified ID and name.
            /// </summary>
            /// <param name="id">The unique identifier for the account.</param>
            /// <param name="name">The name of the account holder.</param>
            void AddAccount(string id, string name);

            /// <summary>
            /// Deposits the specified amount of money into the account with the given ID.
            /// </summary>
            /// <param name="id">The unique identifier for the account.</param>
            /// <param name="amount">The amount of money to deposit.</param>
            void DepositMoney(string id, double amount);

            /// <summary>
            /// Withdraws the specified amount of money from the account with the given ID.
            /// </summary>
            /// <param name="id">The unique identifier for the account.</param>
            /// <param name="amount">The amount of money to withdraw.</param>
            void WithdrawMoney(string id, double amount);

            /// <summary>
            /// Retrieves the details of the account with the given ID.
            /// </summary>
            /// <param name="id">The unique identifier for the account.</param>
            /// <returns>The account with the specified ID, or null if not found.</returns>
            Account GetAccountDetails(string id);
        }
    }

}
