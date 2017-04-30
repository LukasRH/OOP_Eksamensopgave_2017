using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_ExamClassLibary
{
    public class Product : IComparable<Product>, IComparable
    {
        private static int _nextProductId = 1;

        private string _name;

        public Product(string[] fileData) : this(fileData[1].Trim(), Convert.ToDecimal(fileData[2].Trim())/100, Convert.ToBoolean(Convert.ToInt32(fileData[3].Trim())))
        { }

        public  Product(string name, decimal price) : this(name, price, true)
        { }

        public Product(string name, bool canBeBoughtOnCredit, decimal price) : this(name, price, true, canBeBoughtOnCredit)
        { }

        public Product(string name, decimal price, bool active) : this(name, price, active, false)
        { }

        public Product(string name, decimal price, bool active, bool canBeBoughtOnCredit)
        {
            this.Id = _nextProductId;
            _nextProductId++;

            this.Name = name;
            this.Price = price;
            this.Active = active;
            this.CanBeBoughtOnCredit = canBeBoughtOnCredit;

        }

        public int Id { get; }

        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(Product.Name));
        }

        public decimal Price { get; set; }

        public virtual bool Active { get; set; }

        public bool CanBeBoughtOnCredit { get; set; }

        public override string ToString()
        {
            return $"{Id}, {Name}, {Price}";
        }

        public override bool Equals(object obj)
        {

            if (obj is Product newObj)
            {
                return this.GetHashCode() == newObj.GetHashCode();
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return $"Product{this.Id}".GetHashCode();
        }

        public virtual int CompareTo(Product other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Id.CompareTo(other.Id);
        }

        public virtual int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is Product)) throw new ArgumentException($"Object must be of type {nameof(Product)}");
            return CompareTo((Product) obj);
        }
    }
}
