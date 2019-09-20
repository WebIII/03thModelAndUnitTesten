namespace Banking.Models.Domain
{
    class SavingsAccount : BankAccount
    {
        #region Fields
        protected const decimal WithdrawCost = 0.25M;
        #endregion

        #region Properties
        public decimal InterestRate { get; }
        #endregion

        #region Constructors
        public SavingsAccount(string bankAccountNumber, decimal interestRate)
            : base(bankAccountNumber)
        {
            InterestRate = interestRate;
        }
        #endregion

        #region Methods
        public override void Withdraw(decimal amount)
        {
            base.Withdraw(amount);
            base.Withdraw(WithdrawCost);
        }

        public void AddInterest()
        {
            Deposit(Balance * InterestRate);
        }
        #endregion
    }
}
