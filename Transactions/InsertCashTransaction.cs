using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet.Transactions
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(int id, User user, DateTime date, float amount) 
            : base(id, user, date, amount)
        {
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
