using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet.Transactions
{
    public class Transaction
    {
        public Transaction(int transactionId, User user, DateTime date, float amount)
        {
            TransactionId = transactionId;
            User = user;
            Date = date;
            Amount = amount;

            Execute(user, Amount);
        }

        public int TransactionId{ get; }
        public User User{ get; }
        public DateTime Date { get; }
        public float Amount { get; }

        public override string ToString()
        {
            return "Transaction: Id: " + TransactionId.ToString() + " | User:" + User.UserName + " | Amount: " + Amount.ToString() + " | Date/Time: " + Date.ToString();
        }

        public virtual void Execute(User user, float amount){}

    }
}
