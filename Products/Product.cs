using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet
{
    public class Product
    {

        public Product(int id, string name, decimal price, bool active = true, bool creditStatus = false)
        {
            Id = id;
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentException("string: Name can't be null or empty") : name;
            Price = price;

            Active = active;
            CanBeBoughtOnCredit = creditStatus;
        }

        public int Id { get; }
        public decimal Price { get; set; }
        public string Name { get; }

        public bool Active { get; set; }
        public bool CanBeBoughtOnCredit { get; set; }

        public override string ToString()
        {
            return string.Format("|{0, -10}|{1, -60}|{2, 10}|", Id.ToString(), Name, Price + " kr.");
        }

    }
}
