using System;

namespace Banking.Models.Domain
{
    class Transaction
    {
        #region Properties
        public DateTime DateOfTrans { get; }
        public TransactionType TransactionType { get; }
        public decimal Amount { get; }
        #endregion

        #region Constructors
        public Transaction(decimal amount, TransactionType type)
        {
            Amount = amount;
            TransactionType = type;
            DateOfTrans = DateTime.Today;
        }
        #endregion

        #region Methods
        public bool IsWithdraw
        {
            get { return TransactionType == TransactionType.Withdraw; }
        }

        public bool IsDeposit
        {
            get { return TransactionType == TransactionType.Deposit; }
        }
        #endregion
    }
}

