using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ExamClassLibary
{
    //Delegate for the balance warning event.
    public delegate void UserBalanceNotification(User user, decimal balance);

    public class User : IComparable<User>, IComparable
    {
        private static int _nextUserId = 1;

        private string _firstname;
        private string _lastname;
        private string _username;
        private string _email;

        //Constructor for user file data, must be in the form id,firstname,lastname,email,balance
        public User(string[] fileData) : this(fileData[1].Trim(), fileData[2].Trim(), fileData[3].Trim(), fileData[4].Trim(), Convert.ToDecimal(fileData[5].Trim()))
        { }

        public User(string firstname, string lastname, string username, string email) : this(firstname, lastname,
            username, email, 0)
        { }

        public User(string firstname, string lastname, string username, string email, decimal balance)
        {
            this.Id = _nextUserId;
            _nextUserId++;

            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Username = username;
            this.Email = email;
            this.Balance = balance;
        }

        public int Id { get; }

        //Validate that firstname only contain legal characters, before it gets set
        public string Firstname
        {
            get => _firstname;
            set
            {
                if (string.IsNullOrEmpty(value) || !value.All(c => char.IsLetter(c) || c == '-')) throw new ArgumentException("Invalid characters in name");
                _firstname = value;
            }
        }

        //Validate tha last name only contain legal characters, before it gets set
        public string Lastname
        {
            get => _lastname;
            set
            {
                if (string.IsNullOrEmpty(value) || !value.All(c => char.IsLetter(c) || c == ' ')) throw new ArgumentException("Invalid characters in name");
                _lastname = value;
            }
        }

        //Check that the username dont contain any ilegal characters, before it gets set
        public string Username
        {
            get => _username;
            set
            {
                if (string.IsNullOrEmpty(value) || value.Any(char.IsUpper) || value.Any(c => !char.IsLetterOrDigit(c) && c != '_')) throw new ArgumentException($"Username contains one or more ilegal characters");
                _username = value;
            } 
            
        }

        //check if its a valid email before it gets set
        public string Email
        {
            get => _email;
            set
            {
                if (!_validateEmail(value)) throw new ArgumentException($"Invalid email address");
                _email = value;
            }
            
        }

        public decimal Balance { get; set; }

        //Check is a given string is a valid email
        private static bool _validateEmail(string email)
        {
            //Stop if the sting does not contain a @ or more then one @.
            //then split the sting up in loacl and domain to check seperate.
            if (!email.Contains("@") || email.Count(c => c == '@') > 1) return false;
            var address = email.Split('@');
            var local = address[0];
            var domain = address[1];

            //Check if the local part meet the requiremets, else stop here
            if (local == string.Empty || !local.All(c => char.IsLetterOrDigit(c) || c == '.' || c == '_' || c == '-')) return false;

            //Check if doman contain at least one . else stop here
            if (!domain.Contains(".")) return false;
            
            //check that the domain meets the requirements, else stop here
            if (string.IsNullOrEmpty(domain) || domain.StartsWith("-") || domain.StartsWith(".") || domain.EndsWith(".") || domain.EndsWith("-") ||
                !domain.All(c => char.IsLetterOrDigit(c) || c == '.' || c == '-')) return false;
            
            //and if we made it all the way here, we have a valid email.
            return true;
        }

        public override string ToString()
        {
            return $"{$"{Firstname} {Lastname}",20} {Username,15} {$"({Email})",25}";
        }

        public override bool Equals(object obj)
        {
            //Try to convert the obj to User
            var newObj = obj as User;

            //If not a user, return false. else check if its the same user.
            if (newObj != null)
            {
                return this.GetHashCode() == newObj.GetHashCode();
            }
                return false;
        }

        public override int GetHashCode()
        {
            return $"User{this.Id}".GetHashCode();
        }

        public int CompareTo(User other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Id.CompareTo(other.Id);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is User)) throw new ArgumentException($"Object must be of type {nameof(User)}");
            return CompareTo((User) obj);
        }
    }
}
