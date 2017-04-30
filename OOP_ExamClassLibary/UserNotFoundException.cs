using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ExamClassLibary
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message, string username) : base(message)
        {
            this.Username = username;
        }

        public string Username;

    }
}
