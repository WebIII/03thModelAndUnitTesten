﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Banking.Models.Domain
{
    public class BankAccount : IBankAccount
    {
        #region Fields
        private readonly IList<Transaction> _transactions = new List<Transaction>();
        private string _accountNumber;
        #endregion

        #region Properties
        // public decimal Balance { get; private set; }
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in _transactions)
                {
                    balance = item.IsDeposit ? balance + item.Amount : balance - item.Amount;
                }
                return balance;
            }
        }
        public string AccountNumber
        {
            get
            {
                return _accountNumber;
            }
            private set
            {
                Regex regex = new Regex(@"^(?<bankcode>\d{3})-(?<rekeningnr>\d{7})-(?<checksum>\d{2})$");
                Match match = regex.Match(value);
                if (!match.Success)
                    throw new ArgumentException("Bankaccount number format is not correct", nameof(AccountNumber));
                int getal = int.Parse(match.Groups["bankcode"].Value + match.Groups["rekeningnr"].Value);
                int checksum = int.Parse(match.Groups["checksum"].Value);
                if (getal % 97 != checksum)
                    throw new ArgumentException("97 test of the bankaccount number failed", nameof(AccountNumber));
                _accountNumber = value;
            }
        }

        public int NumberOfTransactions
        {
            get { return _transactions.Count; }
        }
        #endregion

        #region Constructors
        public BankAccount(string account)
        {
            AccountNumber = account;
            //   Balance = Decimal.Zero;
        }
        #endregion

        #region Methods
        public virtual void Withdraw(decimal amount)
        {
            _transactions.Add(new Transaction(amount, TransactionType.Withdraw));
            //Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            _transactions.Add(new Transaction(amount, TransactionType.Deposit));
            //Balance += amount;
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

        public override string ToString()
        {
            return $"{AccountNumber} - {Balance}";
        }

        public override bool Equals(object obj)
        {
            //BankAccount account = obj as BankAccount;
            //if (account == null) return false;

            // using the is operator with pattern matching:
            if (!(obj is BankAccount account)) return false;
            return AccountNumber == account.AccountNumber;
        }

        public override int GetHashCode()
        {
            return AccountNumber?.GetHashCode() ?? 0;
        }
        #endregion
    }
}
