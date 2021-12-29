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
        public BuyTransaction(int transactionId, User user, DateTime date, Product product) 
            : base(transactionId, user, date, product.Price)
        {
            Product = product;
        }

        public Product Product { get; }

        public override void Execute()
        {
            User.Balance -= Price;
        }

        public override string ToFileFormat()
        {
            return $"{GetType().Name};{TransactionId};{User.UserName};{Date};{Price};{Product.Id}";
        }

        public override string ToString()
        {
            return $"Buy transaction | Transaction Id:  {TransactionId} | User: {User.UserName} | Product: {Product.Name} | Price: {Price} | Date/Time: {Date}";
        }
    }
}
