using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stregsystemet
{
    public class User : IComparable<User>
    {

        public User(int id, string firstname, string lastname, string username, string email, decimal initialBalance = 0)
        {
            Id = id;
            FirstName = string.IsNullOrEmpty(firstname) ? throw new ArgumentNullException("String: First Name can't be null or empty") : firstname;
            LastName = string.IsNullOrEmpty(lastname) ? throw new ArgumentNullException("String: Last Name can't be null or empty") : lastname;

            UserName = ValidateUserName(username) ? username : throw new ArgumentException("Invalid Username - valid characters: [0-9], [a-z] and '_'");
            Email = ValidateEmail(email) ? email : throw new ArgumentException("Invalid Email");
            Balance = initialBalance;
        }

        public int Id { get; }

        public string FirstName { get; }

        public string LastName { get; }
        public string UserName { get; }

        public string Email { get; }

        public decimal Balance { get; set; }

        public int CompareTo(User user)
        {
            return Id - user.Id;
        }

        public override string ToString()
        {
            return $"User: {FirstName} {LastName} ({Email}) | Balance: {Balance}";
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

            string[] parts = email.ToLower().Split('@');

            if (parts.Length != 2)
            {
                return false;
            }

            string localPart = parts[0];
            string domainPart = parts[1];

            foreach (char character in localPart)
            {
                if (!validCharacters.Contains(character))
                {
                    return false;
                }
            }

            validCharacters = validCharacters.Remove(validCharacters.IndexOf('_'), 1);

            if ((domainPart[0] == '.' || domainPart[0] == '-') && (domainPart[domainPart.Length-1] == '.' || domainPart[domainPart.Length - 1] == '-'))
            {
                return false;
            }

            foreach (char character in domainPart)
            {
                if (!validCharacters.Contains(character))
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            User user = (User)obj;

            if (Id == user.Id)
            {
                return true;
            }

            return false;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return HashCode.Combine(Id, UserName);
        }

    }
}
