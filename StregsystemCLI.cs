using Stregsystemet.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet
{
    public delegate void StregsystemEvent();

    public class StregsystemCLI : IStregsystemUI
    {
        public event StregsystemEvent commandentered;

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

        public void Start()
        {
            foreach (var item in Stregsystem.ActiveProducts)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
