using Banking.Models.Domain;
using System;

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
        }
    }
}
