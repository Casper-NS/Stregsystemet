using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Stregsystemet.Transactions;

namespace Stregsystemet
{
    class StregsystemCommandParser
    {
        private Dictionary<string, Action<string[]>> _adminCommands = new Dictionary<string, Action<string[]>>();

        private StregsystemController _sc;

        public StregsystemCommandParser(StregsystemController sc)
        {
            _sc = sc;
            _adminCommands.Add(":quit", args => _sc.CLI.Close());
            _adminCommands.Add(":activate", args => _sc.Stregsystem.GetProductByID(int.Parse(args[1])).Active = true);
            _adminCommands.Add(":deactivate", args => _sc.Stregsystem.GetProductByID(int.Parse(args[1])).Active = false);
            _adminCommands.Add(":crediton", args => _sc.Stregsystem.GetProductByID(int.Parse(args[1])).CanBeBoughtOnCredit = true);
            _adminCommands.Add(":creditoff", args => _sc.Stregsystem.GetProductByID(int.Parse(args[1])).CanBeBoughtOnCredit = false);
            _adminCommands.Add(":addcredits", args => _sc.Stregsystem.AddCreditsToAccount(_sc.Stregsystem.GetUserByUsername(args[1]), int.Parse(args[2])));
        }

        public void ParseCommand(string command)
        {
            if (String.IsNullOrEmpty(command.Trim()))
            {
                throw new ArgumentException("Command cannot be empty");
            }

            string[] parameters = command.Trim().Split(" ");

            if (parameters.Length > 3)
            {
                _sc.CLI.DisplayTooManyArgumentsError(command);
            }

            if (parameters[0].First() == ':')
            {
                _adminCommands[parameters[0]].Invoke(parameters);
            }
            else
            {
                switch (parameters.Length)
                {
                    case 1:
                        _sc.GetUserinfo(parameters[0]);
                        break;
                    case 2:
                        _sc.Buy(parameters[0], int.Parse(parameters[1]));
                        break;
                    case 3:
                        _sc.Buy(parameters[0], int.Parse(parameters[2]), int.Parse(parameters[1]));
                        break;
                }
            }
        }
    }
}
