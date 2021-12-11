using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet
{
    public class Product
    {

        public Product(int id, string name, int price, bool active, bool creditStatus)
        {
            ID = id;
            Name = name;
            Price = price;

            Active = active;
            CanBeBoughtOnCredit = creditStatus;
        }

        public int ID { get; }
        public int Price { get; set; }
        public string Name { get; }

        public bool Active { get; set; }
        public bool CanBeBoughtOnCredit { get; set; }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

    }
}
