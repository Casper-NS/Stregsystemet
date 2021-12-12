using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet.Transactions
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(int transactionId, User user, DateTime date, float amount) 
            : base(transactionId, user, date, amount)
        {
        }

        public override string ToString()
        {
            return "Insert cash transaction - Id: " + TransactionId.ToString() + " | User: " + User.UserName + " | Amount: " + Amount.ToString() + " | Date/Time: " + Date.ToString();
        }

        public override void Execute(User user, float amount)
        {
            if (amount > 0)
            {
                user.Balance += amount;
            }
            else
            {
                throw new ArgumentException("Amount to be inserted has to be greater than 0");
            }
        }
    }
}
