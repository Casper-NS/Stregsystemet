using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet
{
    class StregsystemCommandParser
    {
        private Dictionary<string, Delegate> _adminCommands = new Dictionary<string, Delegate>();

        private StregsystemController _stregsystemController;

        public StregsystemCommandParser(StregsystemController stregsystemController)
        {
            _stregsystemController = stregsystemController;
            _adminCommands.Add(":quit", new Action(_stregsystemController.CLI.Close));
            _adminCommands.Add(":activate", new Action<int>(Id => _stregsystemController.Stregsystem.GetProductByID(Id).Active = true));
        }

        public void ParseCommand(string command)
        {
            string[] parameters = command.Split(" ");

            if (parameters.Length > 3)
            {
                throw new ArgumentException("Too many arguments");
            }

            //User user;
            //Product product;

            if (parameters[0].Contains(":"))
            {
                switch (parameters[0].ToLower())
                {
                    case ":quit":
                        _stregsystemController.CLI.Close();
                        break;
                    case ":activate":
                        _stregsystemController.Stregsystem.GetProductByID(int.Parse(parameters[1])).Active = true;
                        //_adminCommands[parameters[0]].DynamicInvoke(parameters[1]);
                        break;
                    case ":deactivate":
                        _stregsystemController.Stregsystem.GetProductByID(int.Parse(parameters[1])).Active = false;
                        break;
                    case ":crediton":
                        _stregsystemController.Stregsystem.GetProductByID(int.Parse(parameters[1])).CanBeBoughtOnCredit = true;
                        break;
                    case ":creditoff":
                        _stregsystemController.Stregsystem.GetProductByID(int.Parse(parameters[1])).CanBeBoughtOnCredit = false;
                        break;
                    default:
                        _stregsystemController.CLI.DisplayAdminCommandNotFoundMessage(command);
                        break;
                }
            }
            else
            {
                User user = _stregsystemController.Stregsystem.GetUserByUsername(parameters[0]);
                Product product = _stregsystemController.Stregsystem.GetProductByID(int.Parse(parameters[1]));
                _stregsystemController.Stregsystem.BuyProduct(user, product);
            }

        }
    }
}
