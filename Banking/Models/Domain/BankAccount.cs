using System;
using System.Collections.Generic;

namespace Banking.Models.Domain
{
    class BankAccount
    {
        #region Fields
        private readonly IList<Transaction> _transactions = new List<Transaction>();
        #endregion

        #region Properties
        public decimal Balance { get; private set; }

        public string AccountNumber { get; }

        public int NumberOfTransactions
        {
            get { return _transactions.Count; }
        }
        #endregion

        #region Constructors
        public BankAccount(string account)
        {
            AccountNumber = account;
            Balance = Decimal.Zero;
        }
        #endregion

        #region Methods
        public void Withdraw(decimal amount)
        {
            _transactions.Add(new Transaction(amount, TransactionType.Withdraw));
            Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            _transactions.Add(new Transaction(amount, TransactionType.Deposit));
            Balance += amount;
        }

        public IEnumerable<Transaction> GetTransactions(DateTime? from, DateTime? till)
        {
            if (from == null && till == null) return _transactions;
            if (from is null) from = DateTime.MinValue;
            if (!till.HasValue) till = DateTime.MaxValue;

            IList<Transaction> transList = new List<Transaction>();
            foreach (Transaction t in _transactions)
            {
                if (t.DateOfTrans >= from && t.DateOfTrans <= till)
                    transList.Add(t);
            }
            return transList;
        }
        #endregion
    }
}
