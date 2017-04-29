using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ExamClassLibary
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, decimal amount) : base(user, amount)
        {
        }

        public override string ToString()
        {
            return $"Deposit {Id}: {User}, {Amount}, {Date}";
        }

        public override int GetHashCode()
        {
            return $"DepositTransaction{this.Id}".GetHashCode();
        }

        public override void Execute()
        {
            User.Balance += Amount;
        }
    }
}
