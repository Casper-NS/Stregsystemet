using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet.Exceptions
{
    public class InsufficientCreditsExeption : Exception
    {
        public InsufficientCreditsExeption(User user, Product product, int count)
        {
            User = user;
            Product = product;
            Count = count;
        }

        public User User { get; }
        public Product Product { get; }
        public int Count { get; }
    }
}
