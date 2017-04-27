using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Eksamensopgave_2017
{
    class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException(string message, User user, Product product) : base(message)
        {
            User = user;
            Product = product;
        }

        public User User;
        public Product Product;
    }
}
