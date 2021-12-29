using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet.Transactions
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(int transactionId, User user, DateTime date, decimal price) 
            : base(transactionId, user, date, price)
        {
        }

        public override string ToString()
        {
            return "Insert cash transaction | Transaction Id: " + TransactionId.ToString() + " | User: " + User.UserName + " | Price: " + Price.ToString() + " | Date/Time: " + Date.ToString();
        }

        public override string ToFileFormat()
        {
            return $"{GetType().Name};{TransactionId};{User.UserName};{Date};{Price}";
        }

        public override void Execute()
        {
            if (Price > 0)
            {
                User.Balance += Price;
            }
            else
            {
                throw new ArgumentException("Price to be inserted has to be greater than 0");
            }
        }
    }
}
