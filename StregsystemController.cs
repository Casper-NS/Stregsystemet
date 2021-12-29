using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stregsystemet.Exceptions;
using Stregsystemet.Transactions;

namespace Stregsystemet
{
    public class StregsystemController
    {

        public IStregsystem Stregsystem { get; }
        public IStregsystemUI CLI { get; }

        private StregsystemCommandParser CommandParser;

        public StregsystemController(IStregsystemUI cli, IStregsystem stregsystem)
        {
            CLI = cli;
            Stregsystem = stregsystem;
            CommandParser = new StregsystemCommandParser(this);

            CLI.CommandEntered += OnCommandEntered;

        }

        public void GetUserinfo(string username)
        {
            User user = Stregsystem.GetUserByUsername(username);
            CLI.DisplayUserInfo(user);
        }


        public List<BuyTransaction> Buy(string username, int productId, int count = 1)
        {
            List<BuyTransaction> transactions = new List<BuyTransaction>();

            if (count < 1)
            {
                throw new ArgumentException("It is not possible to buy less than 1 of a product");
            }

            Product product = Stregsystem.GetProductByID(productId);
            User user = Stregsystem.GetUserByUsername(username);
            if (user.Balance < product.Price * count && !product.CanBeBoughtOnCredit)
            {
                throw new InsufficientCreditsExeption(user, product, count);
            }

            if (!product.Active)
            {
                throw new ArgumentException($"Product {productId} is not active and can therefore not be purchaced");
            }

            if (count > 1)
            {
                for (int i = 0; i < count; i++)
                {
                    transactions.Add(Stregsystem.BuyProduct(user, product));
                }
                CLI.DisplayUserBuysProduct(count, transactions.Last());
            }
            else
            {
                transactions.Add(Stregsystem.BuyProduct(user, product));
                CLI.DisplayUserBuysProduct(transactions.First());
            }

            return transactions;
        }

        public void OnCommandEntered(string command)
        {
            try
            {
                CommandParser.ParseCommand(command);
            }
            catch (UserNotFoundException userNotFound)
            {
                CLI.DisplayUserNotFound(userNotFound.UserName);
            }
            catch (ProductNotFoundException productNotFound)
            {
                CLI.DisplayProductNotFound(productNotFound.ProductId.ToString());
            }
            catch (InsufficientCreditsExeption insufficientCredits)
            {
                CLI.DisplayInsufficientCash(insufficientCredits.User, insufficientCredits.Product, insufficientCredits.Count);
            }
            catch (ArgumentNullException nullException)
            {
                CLI.DisplayGeneralError(nullException.Message);
            }
            catch (ArgumentException argException)
            {
                CLI.DisplayGeneralError(argException.Message);
            }
            catch (FormatException formatException)
            {
                CLI.DisplayGeneralError(formatException.Message);
            }
            catch (KeyNotFoundException)
            {
                CLI.DisplayAdminCommandNotFoundMessage(command);
            }
        }

    }
}
