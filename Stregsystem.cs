﻿using Stregsystemet.Exceptions;
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
        public IEnumerable<Product> ActiveProducts => _products.Where(p => p.Active == true);

        private int _transactionIdCounter = 1;
        private IEnumerable<User> _users => GetUsersFromFile("users.csv");
        private IEnumerable<Product> _products => GetProductsFromFile("products.csv");

        private List<Transaction> _transactions;

        public event UserBalanceNotification UserBalanceWarning;

        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(_transactionIdCounter++, user, DateTime.Now, amount);
            _transactions.Add(transaction);
            return transaction;
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction(_transactionIdCounter++, user, DateTime.Now, product.Price);
            _transactions.Add(transaction);
            return transaction;
        }

        public Product GetProductByID(int id)
        {
            if (_products.Where(p => p.ID == id).Any())
            {
                return _products.Where(p => p.ID == id).First();
            }
            else
            {
                throw new ProductNotFoundException(id);
            }
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return _transactions.Where(t => t.User == user).Take(count);
        }

        public User GetUserByUsername(string username)
        {
            if (_users.Where(user => user.UserName == username).Any())
            {
                return _users.Where(user => user.UserName == username).First();
            }
            else
            {
                throw new UserNotFoundException(username);
            }
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
