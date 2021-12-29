using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet.Transactions
{
    public abstract class Transaction
    {
        public Transaction(int transactionId, User user, DateTime date, decimal price)
        {
            TransactionId = transactionId;
            User = user;
            Date = date;
            Price = price;
        }

        public int TransactionId{ get; }
        public User User{ get; }
        public DateTime Date { get; }
        public decimal Price { get; }

        public override string ToString()
        {
            return $"Transaction | Transaction Id: {TransactionId} | User: {User.UserName} | Price: {Price} | Date/Time: {Date}";
        }

        public abstract string ToFileFormat();
        public abstract void Execute();

    }
}
