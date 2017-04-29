using System;
using System.IO;
using System.Collections.Generic;
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

        public LineSystem()
        {
            _loadProductsFromFile(ref _allProducts);
            _loadUsersFromFile(ref _users);
        }

        public IEnumerable<Product> ActiveProducts
        {
            get { return _allProducts.Where(p => p.Active); }
        }

        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            throw new NotImplementedException();
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByID(int id)
        {
            return _allProducts.Find(p => p.Id == id);
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            throw new NotImplementedException();
        }

        public User GetUsers(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            return _users.Find(p => p.Username == username);
        }

        public event User.UserBalanceNotification UserBalanceWarning;

        private void _loadProductsFromFile(ref List<Product> productsOut)
        {
            List<string[]> products = _getAllItemsFromFile("products.csv");
            foreach (string[] product in products)
            {
                productsOut.Add(new Product(product));
            }
        }
        private void _loadUsersFromFile(ref List<User> usersOut)
        {
            List<string[]> users = _getAllItemsFromFile("users.csv");
            foreach (string[] user in users)
            {
                usersOut.Add(new User(user));
            }
        }

        private static List<string[]> _getAllItemsFromFile(string filepath)
        {
            List<string[]> items = new List<string[]>();
            var lines = File.ReadAllLines(filepath);
            foreach (var line in lines.Skip(1).ToList())
            {
                items.Add(_stripHTML(line.Replace('"', ' ')).Split(';'));
            }

            return items;
        }

        private static string _stripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        private static void LogTransaction(Transaction transaction)
        {
            using (StreamWriter sWriter = File.AppendText("Transactions.log"))
            {
                sWriter.WriteLine(transaction.ToString());
            }
        }
    }
}
