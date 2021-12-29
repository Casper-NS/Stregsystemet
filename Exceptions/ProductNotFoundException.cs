using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int productId)
        {
            ProductId = productId;
        }

        public int ProductId { get; }
    }
}
