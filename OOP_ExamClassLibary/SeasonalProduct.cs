using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ExamClassLibary
{
    class SeasonalProduct : Product
    {
        private DateTime _startDate;
        private DateTime _endDate;

        public SeasonalProduct(string name, decimal price, DateTime startDate, DateTime endDate) : this(name, price, startDate, endDate, false)
        {
        }

        public SeasonalProduct(string name, decimal price, DateTime startDate, DateTime endDate, bool canBeBoughtOnCredit) : base(name, price, canBeBoughtOnCredit)
        {
            this._startDate = startDate;
            this._endDate = endDate;
        }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public override bool Active
        {
            get => DateTime.Today.CompareTo(StartDate) >= 0 && DateTime.Today.CompareTo(EndDate) <= 0;
            set => Active = value;
        }
    }
}
