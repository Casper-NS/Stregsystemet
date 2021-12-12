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
        public IEnumerable<Product> ActiveProducts => GetProductsFromFile("products.csv");

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

            string[] tagsToRemove = { "<h1>", "<h2>", "<h3>", "<b>", "</h1>", "</h2>", "</h3>", "</b>" };

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

                    if (DateTime.TryParse(values[4], out DateTime result))
                    {
                        var deActivateDate = result;
                    }

                    foreach (string tag in tagsToRemove)
                    {
                        name.Replace(tag, "");
                    }

                    products.Add(new Product(id, name, price, active));

                }
            }

            return products;
        }

    }
}
