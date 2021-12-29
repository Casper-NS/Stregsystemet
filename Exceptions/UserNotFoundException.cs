using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string username)
        {
            UserName = username;
        }

        public string UserName{ get; }
    }
}
