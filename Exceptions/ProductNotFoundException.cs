using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet.Exceptions
{
    class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int productId)
            : base($"Product with id-[{productId}] not found")
        {
        }
    }
}
