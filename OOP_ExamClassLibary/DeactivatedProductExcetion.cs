using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ExamClassLibary
{
    class DeactivatedProductExcetion : Exception
    {
         public DeactivatedProductExcetion(string message, Product product) : base(message)
        {
            this.Product = product;
        }

        public Product Product;
    }
}
