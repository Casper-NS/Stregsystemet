using Stregsystemet.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet
{
    public class StregsystemCLI : IStregsystemUI
    {
        public delegate void StregsystemEvent(string command);

        public event StregsystemEvent CommandEntered;

        protected virtual void OnCommandEntered(string command)
        {
            if (CommandEntered != null)
            {
                CommandEntered(command);
            }
        }

        public IStregsystem Stregsystem { get; }

        public StregsystemCLI(IStregsystem stregsystem)
        {
            Stregsystem = stregsystem;
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"User [{username}] not found!");
        }

        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"Product [{product}] not found!");
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine(user);
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
            Console.WriteLine(count.ToString() + " * " + transaction);
        }

        public void Close()
        {
            Environment.Exit(0);
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.WriteLine($"User: [{user}] has insufficient funds to purchace [{product}]");
        }

        public void DisplayGeneralError(string errorstring)
        {
            Console.WriteLine($"Error: {errorstring}");
        }

        private void DisplayActiveProducts()
        {
            Console.WriteLine("Input your username and a product ID (seperated by a space) to purchase a product.");

            Console.WriteLine(new string('-', 84));
            Console.WriteLine(string.Format("|{0, -10}|{1, -60}|{2, -10}|", "Id", "Name", "Price"));
            Console.WriteLine(new string('-', 84));
            foreach (var item in Stregsystem.ActiveProducts)
            {
                Console.WriteLine(item.ToString());
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
                OnCommandEntered(command);
            }
        }
    }
}
