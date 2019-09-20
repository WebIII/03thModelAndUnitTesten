using Banking.Models.Domain;
using System;
using System.Collections.Generic;

namespace Banking
{
    class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount("123-4567890-02");
            Console.WriteLine($"AccountNumber: {account.AccountNumber} ");
            Console.WriteLine($"Balance: {account.Balance} ");
            account.Deposit(200M);
            Console.WriteLine($"Balance after deposit of 200 euros: {account.Balance} ");
            account.Withdraw(100);
            Console.WriteLine($"Balance after withdraw of 100 euros: {account.Balance} ");
            Console.WriteLine($"Number of transactions: {account.NumberOfTransactions}");
            IEnumerable<Transaction> transactions = account.GetTransactions(null, null);
            foreach (Transaction t in transactions)
                Console.WriteLine($"Transaction: {t.DateOfTrans} - {t.Amount} - {t.TransactionType}");
        }
    }
}
