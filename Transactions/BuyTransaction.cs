using Stregsystemet.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet.Transactions
{
    public class BuyTransaction : Transaction
    {
        public BuyTransaction(int transactionId, User user, DateTime date, float amount) 
            : base(transactionId, user, date, amount)
        {
        }

        public override void Execute(User user, float amount)
        {
            if (user.Balance >= amount)
            {
                user.Balance -= amount;
            }
            else
            {
                throw new InsufficientCreditsExeption("Not enough credits for transaction");
            }
        }

        public override string ToString()
        {
            return "Buy transaction: Id: " + TransactionId.ToString() + " | User: " + User.UserName + " | Amount: " + Amount.ToString() + " | Date/Time: " + Date.ToString();
        }
    }
}
