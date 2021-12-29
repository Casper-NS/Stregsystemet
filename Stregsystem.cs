using Stregsystemet.Exceptions;
using Stregsystemet.Transactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet
{

    public class Stregsystem : IStregsystem
    {
        public IEnumerable<Product> ActiveProducts => _products.Where(p => p.Active == true);

        private int _transactionIdCounter = 1;
        private int _userIdCounter;
        private int _productIdCounter;

        private readonly List<User> _users;
        private readonly List<Product> _products;

        private readonly List<Transaction> _transactions = new List<Transaction>();

        public delegate void UserBalanceNotification(User user);
        
        public event UserBalanceNotification UserBalanceWarning;

        public Stregsystem()
        {
            File.Create("transactions.csv").Close();
            using (var writer = new StreamWriter($"../../../Data/transactions.csv"))
            {
                writer.WriteLine("transactiontype;transactionid;user;date;price;product");
            }
            _users = GetUsersFromFile("users.csv");
            _products = GetProductsFromFile("products.csv");

            if (_users.Count != 0)
            {
                _userIdCounter = _users.Last().Id + 1;
            }

            if (_products.Count != 0)
            {
                _productIdCounter = _products.Last().Id + 1;
            }

        }

        public InsertCashTransaction AddCreditsToAccount(User user, int price)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(_transactionIdCounter, user, DateTime.Now, price);
            ExecuteTransaction(transaction);
            return transaction;
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction(_transactionIdCounter, user, DateTime.Now, product);
            ExecuteTransaction(transaction);
            if (user.Balance <= 50)
            {
                UserBalanceWarning(user);
            }
            return transaction;
        }

        public Product GetProductByID(int id)
        {
            if (_products.Any(p => p.Id == id))
            {
                return _products.First(p => p.Id == id);
            }
            else
            {
                throw new ProductNotFoundException(id);
            }
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return _transactions.Where(t => t.User.Equals(user)).Reverse().Take(count);
        }

        public User GetUserByUsername(string username)
        {
            if (_users.Any(user => user.UserName == username))
            {
                return _users.First(user => user.UserName == username);
            }
            throw new UserNotFoundException(username);
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return _users.Where(predicate);
        }

        private List<Product> GetProductsFromFile(string fileName)
        {
            List<Product> products = new List<Product>();

            string[] tagsToRemove = { "<h1>", "<h2>", "<h3>", "<b>", "</h1>", "</h2>", "</h3>", "</b>", "\""};

            using (var reader = new StreamReader($"../../../Data/{fileName}"))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    var id = int.Parse(values[0]);
                    var name = values[1];
                    var price = decimal.Parse(values[2])/100;
                    var active = values[3] == "1";

                    foreach (string tag in tagsToRemove)
                    {
                        name = name.Replace(tag, "");
                    }

                    products.Add(new Product(id, name, price, active));

                }
            }

            return products;
        }

        private List<User> GetUsersFromFile(string fileName)
        {
            List<User> users = new List<User>();

            using (var reader = new StreamReader($"../../../Data/{fileName}"))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var id = int.Parse(values[0]);
                    var firstName = values[1];
                    var lastName = values[2];
                    var userName = values[3];
                    var balance = decimal.Parse(values[4])/100;
                    var email = values[5];

                    users.Add(new User(id, firstName, lastName, userName, email, balance));
                }
            }
            return users;
        }


        public User AddUser(string firstname, string lastname, string username, string email, decimal balance = 0)
        {
            User user = new User(_userIdCounter++, firstname, lastname, username, email, balance);
            _users.Add(user);
            return user;
        }

        public Product AddProduct(string name, decimal price, bool active = true, bool creditStatus = false)
        {
            Product product = new Product(_productIdCounter++, name, price, active, creditStatus);
            _products.Add(product);
            return product;
        }

        private void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            _transactionIdCounter++;
            _transactions.Add(transaction);
            using (var writer = File.AppendText($"../../../Data/transactions.csv"))
            {
                writer.WriteLine(transaction.ToFileFormat());
            }
        }

    }
}
