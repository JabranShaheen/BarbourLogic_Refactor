## Banking System Application

This is a simple console-based banking system application demonstrating account management functionalities such as adding accounts, depositing money, withdrawing money, displaying account details, and transferring money between accounts.
### Prerequisites
•	.NET Core SDK 3.1 or later
•	Visual Studio 2019 or later / Visual Studio Code
### Application Features
1.	Add Account:
	Prompts for an account ID and holder name.
	Creates a new account with an initial balance of zero.
2.	Deposit Money:
	Prompts for an account ID and deposit amount.
	Adds the specified amount to the account balance.
3.	Withdraw Money:
	Prompts for an account ID and withdrawal amount.
	Deducts the specified amount from the account balance if sufficient funds are available.
4.	Display Account Details:
	Prompts for an account ID.
	Displays the account ID, holder name, and current balance.
5.	Transfer Money:
	Prompts for source account ID, destination account ID, and transfer amount.
	Transfers the specified amount from the source account to the destination account if sufficient funds are available.
6.	Exit:
	Exits the application.

## Assumptions

1.	Unique Account IDs: It is assumed that account IDs are unique.
2.	Account Validation: The application checks if an account exists before performing any operation on it.
3.	Exception Handling: Custom exceptions such as AccountNotFoundException and InsufficientBalanceException are used for handling errors.
4.	Initial Balance: New accounts are created with an initial balance of zero.
5.	Console Input: All inputs are taken via the console interface.
   
#### Project Structure
	BarbourLogic.Abstractions: Contains the interface definitions for repositories and services.
	BarbourLogic.Application: Contains application logic and services implementations.
	BarbourLogic.Implementations: Contains the concrete implementations of repositories and services.
	BarbourLogic.Tests: Contains unit tests for the application.

Running the Application
1.	Run the application using the provided setup instructions.
2.	Follow the console prompts to add accounts, deposit, withdraw, display details, and transfer money between accounts.

