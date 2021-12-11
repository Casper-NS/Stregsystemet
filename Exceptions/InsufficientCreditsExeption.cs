using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet.Exceptions
{
    public class InsufficientCreditsExeption : Exception
    {
        public InsufficientCreditsExeption(string message) : base(message)
        {

        }
    }
}
