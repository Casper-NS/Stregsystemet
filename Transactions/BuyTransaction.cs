using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet.Transactions
{
    public class BuyTransaction : Transaction
    {
        public BuyTransaction(int id, User user, DateTime date, float amount) 
            : base(id, user, date, amount)
        {
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
