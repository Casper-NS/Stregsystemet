using Stregsystemet.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet
{
    public interface IStregsystemUI
    {
        void displayusernotfound(string username);
        void displayproductnotfound(string product);
        void displayuserinfo(User user);
        void displaytoomanyargumentserror(string command);
        void displayadmincommandnotfoundmessage(string admincommand);
        void displayuserbuysproduct(BuyTransaction transaction);
        void displayuserbuysproduct(int count, BuyTransaction transaction);
        void close();
        void displayinsufficientcash(User user, Product product);
        void displaygeneralerror(string errorstring);
        void start();
        event StregsystemEvent commandentered;
    }

}
