using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Domain.Entities
{
    public class Account : BaseEntity
    {
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public decimal CreditLimit { get; set; }

        public Account(int id, int userId, decimal balance, decimal creditLimit)
        {
            this.Id = id;
            this.UserId = userId;
            this.Balance = balance;
            this.CreditLimit = creditLimit;
        }

        public void Debit(decimal amount)
        {
            var newBalance = Balance - amount;

            if (newBalance + CreditLimit < 0)
            {
                throw new Exception("This account does not have enough funds.");
            }

            Balance = newBalance;
        }

        public void Credit(decimal amount)
        {
            Balance += amount;
        }
    }
}
