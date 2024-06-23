using BarbourLogic.Abstractions.Entities;

namespace BarbourLogic.Abstractions.Repository
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Adds a new account to the repository.
        /// </summary>
        /// <param name="account">The account to add.</param>
        void AddAccount(Account account);

        /// <summary>
        /// Retrieves an account by its ID.
        /// </summary>
        /// <param name="id">The ID of the account to retrieve.</param>
        /// <returns>The account with the specified ID, or null if not found.</returns>
        Account GetAccountById(string id);

        /// <summary>
        /// Updates an existing account in the repository.
        /// </summary>
        /// <param name="account">The account to update.</param>
        void UpdateAccount(Account account);
    }
}
