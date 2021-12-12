using Stregsystemet.Transactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet
{
    public delegate void UserBalanceNotification();

    public class Stregsystem : IStregsystem
    {
        public IEnumerable<Product> ActiveProducts => Products.Where(p => p.Active == true);

        private IEnumerable<User> Users => GetUsersFromFile("users.csv");
        private IEnumerable<Product> Products => GetProductsFromFile("products.csv");

        public event UserBalanceNotification UserBalanceWarning;

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
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public User GetUsers(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
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
                    var price = int.Parse(values[2]);
                    var active = values[3] == "1" ? true : false;

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
                    var balance = float.Parse(values[4]);
                    var email = values[5];

                    users.Add(new User(id, firstName, lastName, userName, email, balance));

                }
            }
            return users;
        }
    }
}
