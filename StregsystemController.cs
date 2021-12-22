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

        private StregsystemCommandParser CommandParser;

        public StregsystemController(IStregsystemUI cli, IStregsystem stregsystem)
        {
            Stregsystem = stregsystem;
            CLI = cli;
            CommandParser = new StregsystemCommandParser(this);

            CLI.CommandEntered += this.OnCommandEntered;

        }

        public void Buy(string username, string productId)
        {
            User user = Stregsystem.GetUserByUsername(username);
            if (int.TryParse(productId, out int Id))
            {
                Product product = Stregsystem.GetProductByID(Id);
                Stregsystem.BuyProduct(user, product);
            }
            else
            {
                //TODO: Make this a proper exception.
                throw new Exception($"Could not parse {productId} to an int");
            }
            
            
        }

        public void OnCommandEntered(string command)
        {
            CommandParser.ParseCommand(command);
        }

    }
}
