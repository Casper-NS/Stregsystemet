using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet
{
    public class Product
    {

        public Product(int id, string name, int price, bool active = true, bool creditStatus = false)
        {
            ID = id;
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentException("string: Name can't be null or empty") : name;
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
            //return ID.ToString() + " - " + Name + " - " + Price.ToString() + " kr."; 
            return string.Format("|{0, -10}|{1, -60}|{2, 10}|", ID.ToString(), Name, Price.ToString() + " kr.");
        }

    }
}
