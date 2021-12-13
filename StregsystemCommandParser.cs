using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet
{
    class StregsystemCommandParser
    {
        private StregsystemController _stregsystemController;

        public StregsystemCommandParser(StregsystemController stregsystemController)
        {
            _stregsystemController = stregsystemController;
        }

        public void ParseCommand(string command)
        {
            string[] parameters = command.Split(" ");

            if (parameters[0].Contains(":"))
            {
                switch (parameters[0].ToLower())
                {
                    case ":quit":
                        _stregsystemController.CLI.Close();
                        break;
                    case ":activate":
                        _stregsystemController.Stregsystem.GetProductByID(int.Parse(parameters[1])).Active = true;
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
