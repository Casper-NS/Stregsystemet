using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet.Transactions
{
    public class Transaction
    {
        public Transaction(int id, User user, DateTime date, float amount)
        {
            ID = id;
            User = user;
            Date = date;
            Amount = amount;
        }

        public int ID{ get; }
        public User User{ get; }
        public DateTime Date { get; }
        public float Amount { get; }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public virtual void Execute()
        {
            throw new NotImplementedException();
        }

    }
}
