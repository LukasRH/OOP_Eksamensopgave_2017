using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ExamClassLibary
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, decimal amount) : base("Deposit", user, amount)
        {
            BalanceAfterTransaction = User.Balance + Amount;
        }

        public override string DisplayTransaction()
        {
            return $"{"Deposit",-10} {"Add funds to account",-40} {Amount,4}DDK {BalanceAfterTransaction,8}DDK   {Date:HH:mm:ss}";
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
