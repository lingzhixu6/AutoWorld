using System;

namespace UI.Market
{
    public abstract class Employee
    {
        private string _type;
        private decimal _salary;
        
        public string GetJobType()
        {
            return _type;
        }

        protected void SetType(string materialName)
        {
            this._type = materialName;
        }

        public decimal GetSalary()
        {
            _salary = Math.Round(_salary,2);
            return _salary;
        }

        protected void SetSalary(decimal price)
        {
            _salary = price;
        }
    }
}