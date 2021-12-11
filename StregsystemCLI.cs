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

        public void close()
        {
            throw new NotImplementedException();
        }

        public void displayadmincommandnotfoundmessage(string admincommand)
        {
            throw new NotImplementedException();
        }

        public void displaygeneralerror(string errorstring)
        {
            throw new NotImplementedException();
        }

        public void displayinsufficientcash(User user, Product product)
        {
            throw new NotImplementedException();
        }

        public void displayproductnotfound(string product)
        {
            throw new NotImplementedException();
        }

        public void displaytoomanyargumentserror(string command)
        {
            throw new NotImplementedException();
        }

        public void displayuserbuysproduct(BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void displayuserbuysproduct(int count, BuyTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void displayuserinfo(User user)
        {
            throw new NotImplementedException();
        }

        public void displayusernotfound(string username)
        {
            throw new NotImplementedException();
        }

        public void start()
        {
            throw new NotImplementedException();
        }
    }
}
