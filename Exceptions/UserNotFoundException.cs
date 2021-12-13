using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet.Exceptions
{
    class UserNotFoundException : Exception
    {
        public UserNotFoundException(string username)
            : base($"User [{username}] not found")
        {

        }
    }
}
