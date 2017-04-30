using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ExamClassLibary
{
    public class BuyTransaction : Transaction
    {
        public BuyTransaction(User user, Product product) : base("Purchase", user, product.Price)
        {
            Product = product;
            BalanceAfterTransaction = User.Balance - Amount;

        }

        public Product Product { get;}
        
        public override int GetHashCode()
        {
            return $"Purchase{this.Id}".GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString($"{Product.Id,3} {Product.Name}");
        }

        public override string DisplayTransaction()
        {
            return $"{"Purchase",-10} {Product.Name,-40} {Amount,4}DDK {BalanceAfterTransaction,8}DDK   {Date:HH:mm:ss}";
        }

        public override void Execute()
        {
            if (!Product.Active) throw new DeactivatedProductExcetion($"Product not active!", Product);
            if (User.Balance - Amount < 0 && !Product.CanBeBoughtOnCredit) throw new InsufficientCreditsException($"Not enough credits!", User, Product);
            User.Balance -= Amount;
        }
    }
}
