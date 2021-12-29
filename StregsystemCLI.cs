using Stregsystemet.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stregsystemet.Exceptions;

namespace Stregsystemet
{
    public class StregsystemCLI : IStregsystemUI
    {
        public delegate void StregsystemEvent(string command);

        public event StregsystemEvent CommandEntered;

        private IStregsystem Stregsystem { get; }

        public StregsystemCLI(IStregsystem stregsystem)
        {
            Stregsystem = stregsystem;
            Stregsystem.UserBalanceWarning += BalanceWarning;
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"User [{username}] not found!");
        }

        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"Product id [{product}] not found!");
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine(user);
            if (user.Balance <= 50)
            {
                BalanceWarning(user);
            }
            Console.WriteLine("Recent Transactions:");
            foreach (var transaction in Stregsystem.GetTransactions(user, 10))
            {
                Console.WriteLine(transaction);
            }
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"Too many arguments in command: [{command}]");
        }

        public void DisplayAdminCommandNotFoundMessage(string admincommand)
        {
            Console.WriteLine($"Admin command [{admincommand}] not found");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine(transaction);
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine($"{count} * {transaction}");
        }

        public void Close()
        {
            Environment.Exit(0);
        }

        public void DisplayInsufficientCash(User user, Product product, int count = 1)
        {
            Console.WriteLine($"[{user}] has insufficient funds to purchace [{count}*{product.Name}]");
            Console.WriteLine($"Current balance is {user.Balance}, price of purchase is {count*product.Price}");
        }

        public void DisplayGeneralError(string errorstring)
        {
            Console.WriteLine($"Error: {errorstring}");
        }

        public void BalanceWarning(User user)
        {
            Console.WriteLine($"Your balance is low, current balance is {user.Balance}, consider adding more funds.");
        }

        private void DisplayActiveProducts()
        {
            Console.Clear();
            Console.WriteLine("Input your username and a product Id (seperated by a space) to purchase a product.");
            Console.WriteLine("Multi buy is possible by writing ProductId:Number instead of just the product id.");

            Console.WriteLine(new string('-', 84));
            Console.WriteLine("|{0, -10}|{1, -60}|{2, -10}|", "Id", "Name", "Price");
            Console.WriteLine(new string('-', 84));
            foreach (var product in Stregsystem.ActiveProducts)
            {
                Console.WriteLine(product.ToString());
            }
            Console.WriteLine(new string('-', 84));

        }

        public void Start()
        {
            string command;

            DisplayActiveProducts();

            while (true)
            {
                command = Console.ReadLine();
                DisplayActiveProducts();
                CommandEntered(command);
            }
        }
    }
}
