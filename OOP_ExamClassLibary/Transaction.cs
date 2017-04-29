using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ExamClassLibary
{
    public abstract class Transaction
    {
        private static int _nextTransationId = 1;

        protected Transaction(User user, decimal amount)
        {
            this.Id = _nextTransationId;
            _nextTransationId++;

            this.User = user;
            this.Amount = amount;
            this.Date = DateTime.Now;
        }

        public int Id { get; }

        public User User { get; }

        public DateTime Date { get; }

        public decimal Amount { get; }
        
        public abstract void Execute();

        public abstract override string ToString();

        public override bool Equals(object obj)
        {
            var newObj = obj as Transaction;

            if (newObj != null)
            {
                return this.GetHashCode() == newObj.GetHashCode();
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public abstract override int GetHashCode();
    }
}
