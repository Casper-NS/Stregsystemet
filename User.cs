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

            UserName = ValidateUserName(username) ? username : throw new ArgumentException("Invalid Username - valid characters: [0-9], [a-z] and '_'");
            FirstName = string.IsNullOrEmpty(firstname) ? throw new ArgumentNullException("String: First Name can't be null or empty") : firstname;
            LastName = string.IsNullOrEmpty(lastname) ? throw new ArgumentNullException("String: Last Name can't be null or empty") : lastname;

            Email = ValidateEmail(email) ? email : throw new ArgumentException("Invalid Email");
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
            string validCharacters = "abcdefghijklmnopqrstuvwxyz1234567890_";

            foreach(char character in username)
            {
                if (!validCharacters.Contains(character))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidateEmail(string email)
        {
            string validCharacters = "abcdefghijklmnopqrstuvwxyz1234567890_.-";

            string[] parts;
            string localPart;
            string domainPart;

            if (email.Count(c => c == '@') > 1)
            {
                parts = email.Split('@');
                localPart = parts[0];
                domainPart = parts[1];
            }
            else
            {
                return false;
            }

            foreach (char character in localPart)
            {

                if (!validCharacters.Contains(character))
                {
                    return false;
                }
            }

            validCharacters = validCharacters.Remove(validCharacters.IndexOf('_'), 1);

            foreach (char character in domainPart)
            {
                if (!validCharacters.Contains(character))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
