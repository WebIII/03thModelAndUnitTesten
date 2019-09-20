using System.Collections.Generic;
using Banking.Models.Domain;
using System;
using Xunit;

namespace Banking.Tests.Models.Domain
{
    public class BankAccountTransactionTest
    {
        private static readonly string _bankAccountNumber = "123-4567890-02";
        private readonly BankAccount _bankAccount;
        private readonly DateTime _yesterday = DateTime.Today.AddDays(-1);
        private readonly DateTime _tomorrow = DateTime.Today.AddDays(1);

        public BankAccountTransactionTest()
        {
            _bankAccount = new BankAccount(_bankAccountNumber);
        }

        [Fact]
        public void NewAccount_HasZeroTransactions()
        {
            Assert.Equal(0, _bankAccount.NumberOfTransactions);
        }

        [Fact]
        public void Deposit_Amount_AddsTransaction()
        {
            _bankAccount.Deposit(100);
            Assert.Equal(1, _bankAccount.NumberOfTransactions);
            //Test of de toegevoegde transactie de juiste gegevens bevat
            Transaction t = new List<Transaction>(_bankAccount.GetTransactions(DateTime.Today, DateTime.Today))[0];
            Assert.Equal(100, t.Amount);
            Assert.Equal(TransactionType.Deposit, t.TransactionType);
        }

        [Fact]
        public void WithDraw_Amount_AddsTransaction()
        {
            _bankAccount.Withdraw(100);
            Assert.Equal(1, _bankAccount.NumberOfTransactions);
            //Test of de toegevoegde transactie de juiste gegevens bevat
            Transaction t = new List<Transaction>(_bankAccount.GetTransactions(DateTime.Today, DateTime.Today))[0];
            Assert.Equal(100, t.Amount);
            Assert.Equal(TransactionType.Withdraw, t.TransactionType);
        }

        [Fact]
        public void GetTransactions_All_ReturnsAllTransactions()
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            List<Transaction> tt = new List<Transaction>(_bankAccount.GetTransactions(null, null));
            Assert.Equal(2, tt.Count);
        }

        [Fact]
        public void GetTransactions_NoTransactions_ReturnsEmptyList()
        {
            List<Transaction> t = new List<Transaction>(_bankAccount.GetTransactions(null, null));
            Assert.Empty(t);
        }

        [Fact]
        public void GetTransactions_WithinAPeriodThatHasTransactions_ReturnsTransactions()
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            List<Transaction> tt = new List<Transaction>(_bankAccount.GetTransactions(_yesterday, _tomorrow));
            Assert.Equal(2, tt.Count);
        }

        [Fact]
        public void GetTransactions_WithinAPeriodThatHasNoTransactions_ReturnsNoTransactions()
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            List<Transaction> tt = new List<Transaction>(_bankAccount.GetTransactions(_yesterday, _yesterday));
            Assert.Empty(tt);
        }

        [Fact]
        public void GetTransactions_BeforeADateWithTransactions_ReturnsTransactions()
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            List<Transaction> tt = new List<Transaction>(_bankAccount.GetTransactions(null, _tomorrow));
            Assert.Equal(2, tt.Count);
        }

        [Fact]
        public void GetTransactions_BeforeADateWithoutTransactions_ReturnsNoTransactions()
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            List<Transaction> tt = new List<Transaction>(_bankAccount.GetTransactions(null, _yesterday));
            Assert.Empty(tt);
        }

        [Fact]
        public void GetTransactions_AfterADateWithTransactions_ReturnsTransactions()
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            List<Transaction> tt = new List<Transaction>(_bankAccount.GetTransactions(_yesterday,null));
            Assert.Equal(2, tt.Count);
        }

        [Fact]
        public void GetTransactions_AfterADateWithoutTransactions_ReturnsNoTransactions()
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            List<Transaction> tt = new List<Transaction>(_bankAccount.GetTransactions(_tomorrow, null));
            Assert.Empty(tt);
        }       
    }
}

