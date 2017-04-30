using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ExamClassLibary
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message, int productId) : base(message)
        {
            this.ProductId = productId;
        }

        public int ProductId;
    }
}
