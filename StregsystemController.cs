using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet
{
    class StregsystemController
    {

        public IStregsystem Stregsystem { get; }
        public IStregsystemUI CLI { get; }

        public StregsystemController(IStregsystemUI cli, IStregsystem stregsystem)
        {
            Stregsystem = stregsystem;
            CLI = cli;
        }
    }
}
