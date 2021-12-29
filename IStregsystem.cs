using Stregsystemet.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet
{
    public interface IStregsystem
    {
        IEnumerable<Product> ActiveProducts { get; }
        InsertCashTransaction AddCreditsToAccount(User user, int price);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetProductByID(int id);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        IEnumerable<User> GetUsers(Func<User, bool> predicate);
        User GetUserByUsername(string username);

        event Stregsystem.UserBalanceNotification UserBalanceWarning;
    }
}
