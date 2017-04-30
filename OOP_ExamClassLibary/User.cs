using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ExamClassLibary
{
    public delegate void UserBalanceNotification(User user, decimal balance);


    public class User : IComparable<User>, IComparable
    {
        private static int _nextUserId = 1;

        private string _firstname;
        private string _lastname;
        private string _username;
        private string _email;

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

        public string Firstname
        {
            get => _firstname;
            set => _firstname = value ?? throw new ArgumentNullException(nameof(User.Firstname));
        }

        public string Lastname
        {
            get => _lastname;
            set => _lastname = value ?? throw new ArgumentNullException(nameof(User.Lastname));
        }

        public string Username
        {
            get => _username;
            set
            {
                if (value.Any(char.IsUpper) || value.Any(c => !char.IsLetterOrDigit(c) && c != '_')) throw new ArgumentException($"Username contains one or more ilegal characters");
                _username = value;
            } 
            
        }

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

        private static bool _validateEmail(string email)
        {
            if (!email.Contains("@") || email.Count(c => c == '@') > 1) return false;
            var address = email.Split('@');
            var local = address[0];
            var domain = address[1];

            if (!local.Any(c => char.IsLetterOrDigit(c) || c == '.' || c == '_' || c == '-')) return false;

            if (!domain.Contains(".")) return false;

            if (domain.StartsWith("-") || domain.StartsWith(".") || domain.EndsWith(".") || domain.EndsWith("-") ||
                !domain.Any(c => char.IsLetterOrDigit(c) || c == '.' || c == '-')) return false;

            return true;
        }

        public override string ToString()
        {
            return $"{$"{Firstname} {Lastname}",20} {Username,15} {$"({Email})",25}";
        }

        public override bool Equals(object obj)
        {
            var newObj = obj as User;

            if (newObj != null)
            {
                return this.GetHashCode() == newObj.GetHashCode();
            }
            else
            {
                return base.Equals(obj);
            }
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
