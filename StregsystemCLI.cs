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
            throw new NotImplementedException();
        }

        public void DisplayProductNotFound(string product)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserInfo(User user)
        {
            throw new NotImplementedException();
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            throw new NotImplementedException();
        }

        public void DisplayAdminCommandNotFoundMessage(string admincommand)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            throw new NotImplementedException();
        }

        public void DisplayGeneralError(string errorstring)
        {
            throw new NotImplementedException();
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
