using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Eksamensopgave_2017
{
    class BuyTransaction : Transaction
    {
        public BuyTransaction(User user, Product product) : base(user, product.Price)
        {
            Product = product;
        }
        
        public Product Product { get;}

        public override string ToString()
        {
            return $"Purchase {Id}: {User}, {Product.Name}, {Amount}, {Date}";
        }

        public override int GetHashCode()
        {
            return $"BuyTransaction{this.Id}".GetHashCode();
        }

        public override void Execute()
        {
            if (!Product.Active) throw new DeactivatedProductExcetion($"Product not active!", Product);
            if (User.Balance - Amount < 0 && !Product.CanBeBoughtOnCredit) throw new InsufficientCreditsException($"Not enough credits!", User, Product);
            User.Balance -= Amount;
        }
    }
}
