using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ExamClassLibary
{
    class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException(string message, User user, Product product) : base(message)
        {
            this.User = user;
            this.Product = product;
        }

        public User User;
        public Product Product;
    }
}
