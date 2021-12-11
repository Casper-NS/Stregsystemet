using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet
{
    public class User : IComparable<User>
    {

        public User(int id, string username, string firstname, string lastname, string email, int initialBalance = 0)
        {
            ID = id;

            UserName = username;
            FirstName = firstname;
            LastName = lastname;

            Email = email;
            Balance = initialBalance;
        }

        public int ID { get; }

        public string FirstName { get; }

        public string LastName { get; }
        public string UserName { get; }

        public string Email { get; }

        public int Balance { get; }

        public int CompareTo(User user)
        {
            return ID - user.ID;
        }

        public override string ToString()
        {
            return "User: " + FirstName + " " + LastName + " " + $"({Email})";
        }

        private bool ValidateUserName(string username)
        {
            throw new NotImplementedException();
        }

        private bool ValidateEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
