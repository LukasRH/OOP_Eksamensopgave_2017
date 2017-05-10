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
        public string Type { get; }

        protected Transaction(string type, User user, decimal amount)
        {
            this.Type = type;
            this.User = user;
            this.Amount = amount;
            this.Date = DateTime.Now;

            //Create unique using the current time and transaction number.
            this.Id = Convert.ToInt32($"{Date:HHmmss}{_nextTransationId++}");
        }

        //Snapshot of the users balance after transaction
        protected decimal BalanceAfterTransaction;

        public int Id { get; }

        public User User { get; }

        public DateTime Date { get; }

        public decimal Amount { get; }
        
        public abstract void Execute();

        public abstract string DisplayTransaction();

        public override string ToString()
        {
            return $"{Type,-8} {Id,10}: {User,-70} {string.Empty,-40} {Amount}DDK {Date,20}";
        }

        public string ToString(string message)
        {
            return $"{Type,-8} {Id,10}: {User,-70} {message,-40} {Amount}DDK {Date,20}";
        }

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
