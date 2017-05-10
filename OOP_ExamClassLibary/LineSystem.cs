using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOP_ExamClassLibary
{
    public class LineSystem : ILineSystem
    {
        private readonly List<Product> _allProducts = new List<Product>();
        private readonly List<User> _users = new List<User>();
        private List<Transaction> _transactions = new List<Transaction>();

        public LineSystem()
        {
            LoadProductsFromFile(ref _allProducts);
            LoadUsersFromFile(ref _users);
        }

        //Get active products
        public IEnumerable<Product> ActiveProducts
        {
            get { return _allProducts.Where(p => p.Active); }
        }

        //Add cash to a users balance
        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            return ExecuteTransaction(new InsertCashTransaction(user, amount));
        }

        //Buys a product on a users account
        public BuyTransaction BuyProduct(User user, Product product)
        {
            return ExecuteTransaction(new BuyTransaction(user, product));
        }

        //Execute a given transaction, logs the transaction, and invoke the balancewarning
        //if the users balance gets under 50
        private T ExecuteTransaction<T>(T transaction) where T : Transaction
        {
            try
            {
                transaction.Execute();
                _transactions.Add(transaction);
                LogTransaction(transaction, "Completed");
                if (transaction.User.Balance <= 50)
                {
                    UserBalanceWarning?.Invoke(transaction.User, transaction.User.Balance);
                }

                return transaction;
            }
            catch (DeactivatedProductExcetion ex)
            {
                LogTransaction(transaction, $"Failed: {ex.Message}");
                throw;
            }
            catch (InsufficientCreditsException ex)
            {
                LogTransaction(transaction, $"Failed: {ex.Message}");
                throw;
            }
        }

        //finds a product a given id
        public Product GetProductByID(int id)
        {
            Product product = _allProducts.Find(p => p.Id == id);
            if (product != null)
            {
                return product;
            }
            throw new ProductNotFoundException("Product was not found", id);
        }

        //Finds a given amount of transactions made by the given user
        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return _transactions.Where(t => t.User.Equals(user)).OrderByDescending(t => t.Date).Take(count);
        }
        
        //Gets the users that match the given predicate
        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return _users.Where(predicate);
        }

        //Find the user with the given username
        public User GetUserByUsername(string username)
        {
            User user = _users.Find(p => p.Username == username);
            if (user != null)
            {
                if (user.Balance < 50)
                {
                    UserBalanceWarning?.Invoke(user, user.Balance);
                }
                return user;
            }
            throw new UserNotFoundException("User was not found", username);
        }

        //event to broadcast when a users balance is low
        public event UserBalanceNotification UserBalanceWarning;
        
        //Log a transaction to the appropiate file
        private static void LogTransaction(Transaction transaction, string status)
        {
            using (StreamWriter sWriter = File.AppendText($"Transactions_{DateTime.Today:dd-MM-yy}.log"))
            {
                sWriter.WriteLine($"{transaction} : {status}");
            }
        }

        //Add all the product to the program
        private void LoadProductsFromFile(ref List<Product> productsOut)
        {
            List<string[]> products = GetAllItemsFromFile("products.csv");
            foreach (string[] product in products)
            {
                productsOut.Add(new Product(product));
            }
        }

        //Add all useres
        private void LoadUsersFromFile(ref List<User> usersOut)
        {
            List<string[]> users = GetAllItemsFromFile("users.csv");
            foreach (string[] user in users)
            {
                usersOut.Add(new User(user));
            }
        }

        //read the given ; separated file and returns all lines in it.
        private static List<string[]> GetAllItemsFromFile(string filepath)
        {
            List<string[]> items = new List<string[]>();
            var lines = File.ReadAllLines(filepath);
            foreach (var line in lines.Skip(1).ToList())
            {
                //format the line before adding it
                items.Add(StripHTML(line.Replace('"', ' ')).Split(';'));
            }

            return items;
        }

        //Strip html tags
        private static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

    }
}
