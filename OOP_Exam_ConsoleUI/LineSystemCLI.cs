using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_ExamClassLibary;

namespace OOP_Exam_ConsoleUI
{
    class LineSystemCLI : ILineSystemUI
    {
        private ILineSystem _lineSystem;
        private readonly string _title;
        private bool _running = false;
        private string _cliInfoMessage = string.Empty;
        private string _userBalanceWarning = string.Empty;

        public LineSystemCLI(string title, ILineSystem lineSystem)
        {
            this._lineSystem = lineSystem;
            this._title = title;
            lineSystem.UserBalanceWarning += (user, balance) => _userBalanceWarning =
                $"\nUser: {user.Username} balance is low! Current balance: {balance}DDK";
        }

        public void DisplayUserNotFound(string username)
        {
            _cliInfoMessage = $"User {username} not found!";
        }

        public void DisplayProductNotFound(string product)
        {
            _cliInfoMessage = $"Product {product} was not found!";
        }

        public void DisplayUserInfo(User user)
        {
            _cliInfoMessage = string.Empty;
            Console.Clear();
            Console.Title = user.Username;
            Console.WriteLine($"Username: {user.Username}\nFullname: {user.Firstname} {user.Lastname}\nBalance: {user.Balance}DDK");
            Console.WriteLine(_userBalanceWarning);
            var lastTransactions = _lineSystem.GetTransactions(user, 10);
            Console.WriteLine($"Your Last 10 Transactions");
            Console.WriteLine($"{"Type",-10} {"Product",-40} {"Price",6}{string.Empty,5}{"Balance",-10} {"Time",-4}\n");
            foreach (Transaction transaction in lastTransactions)
            {
                Console.WriteLine(transaction.DisplayTransaction());
            }
            Console.ReadLine();
            _userBalanceWarning = string.Empty;
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            _cliInfoMessage = $"To many arguments in command : [{command}]";
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            _cliInfoMessage = $"Admincommand {adminCommand} was not found!";
        }

        public void DisplayAdminCommandMessage(string message)
        {
            _userBalanceWarning = string.Empty;
            _cliInfoMessage = message;
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            _cliInfoMessage = $"Succesfully bought {transaction.Product.Name} for {transaction.Product.Price}DDK on account {transaction.User.Username}";
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            _cliInfoMessage = $"Succesfully bought {count} x {transaction.Product.Name} for {transaction.Product.Price * count}DDK on account {transaction.User.Username}";
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            _cliInfoMessage = $"Insufficient credit on {user.Username}'s account! Account balance: {user.Balance}DDK, Product {product.Name} price {product.Price}kr";
        }

        public void DisplayGeneralError(string errorString)
        {
            _cliInfoMessage = $"Error! {errorString}";
        }

        public void Start()
        {
            _running = true;
            Console.Title = _title;
            do
            {
                DrawCLI();
            } while (_running);
        }

        private void DrawCLI()
        {
            Console.Clear();
            Console.WriteLine("┌────┬─────────────────────────────────────┬──────────┐");
            foreach (var product in _lineSystem.ActiveProducts)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("└────┴─────────────────────────────────────┴──────────┘");
            Console.WriteLine();
            Console.WriteLine(_cliInfoMessage + _userBalanceWarning);
            Console.Write("Quickbuy: ");
            string enteredCommand = Console.ReadLine();
            _userBalanceWarning = string.Empty;
            if (enteredCommand != string.Empty) CommandEntered?.Invoke(this, enteredCommand.Trim());
        }

        public void Close()
        {
            _running = false;
        }

        public event LineSystemEvent CommandEntered;
    }
}
