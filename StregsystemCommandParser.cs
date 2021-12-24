using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Stregsystemet.Transactions;

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

        public int IntParse(string str)
        {
            if (int.TryParse(str, out int result))
            {
                return result;
            }
            else
            {
                _stregsystemController.CLI.DisplayGeneralError("Could not parse an string to integer");
                throw new ArgumentException("string cannot be parsed to int");
            }
        }

        public void ParseCommand(string command)
        {
            string[] parameters = command.Split(" ");

            if (parameters.Length > 3)
            {
                _stregsystemController.CLI.DisplayTooManyArgumentsError(command);
            }




            if (parameters[0].First() == ':')
            {
                switch (parameters[0].ToLower())
                {
                    case ":quit":
                        _stregsystemController.CLI.Close();
                        break;
                    case ":activate":
                        _stregsystemController.Stregsystem.GetProductByID(int.Parse(parameters[1])).Active = true;
                        //_adminCommands[parameters[0]].DynamicInvoke(new object? [] {int.Parse(parameters[1])});
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
                    case ":addcredits":
                        User user = _stregsystemController.Stregsystem.GetUserByUsername(parameters[0]);
                        _stregsystemController.Stregsystem.AddCreditsToAccount(user, IntParse(parameters[1]));
                        break;
                    default:
                        _stregsystemController.CLI.DisplayAdminCommandNotFoundMessage(command);
                        break;
                }
            }
            else if (parameters.Length == 1)
            {
                User user = _stregsystemController.Stregsystem.GetUserByUsername(parameters[0]);
                _stregsystemController.CLI.DisplayUserInfo(user);
            }
            else if (parameters.Length == 2)
            {
                int productId = 0;
                int count = 0;
                User user = _stregsystemController.Stregsystem.GetUserByUsername(parameters[0]);
                if (parameters[1].Contains(':'))
                {
                    string[] productParams = parameters[1].Split(':');
                    if (productParams.Length > 2)
                    {
                        _stregsystemController.CLI.DisplayTooManyArgumentsError(command);
                    }
                    else
                    {
                        productId = IntParse(productParams[0]);
                        count = IntParse(productParams[1]);
                    }
                }
                else
                {
                    productId = IntParse(parameters[1]);
                    count = 1;
                }
                Product product = _stregsystemController.Stregsystem.GetProductByID(productId);
                BuyTransaction transaction = _stregsystemController.Stregsystem.BuyProduct(user, product);
                _stregsystemController.CLI.DisplayUserBuysProduct(count, transaction);
            }
            else
            {
                _stregsystemController.CLI.DisplayGeneralError($"Could not parse command: [{command}]");
            }

        }
    }
}
