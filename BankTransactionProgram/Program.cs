// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;

class BankTransactionProgram
{
    static void Main()
    {
        int initialBalance = 1000;
        int[] transactions = { 200, -150, -900, 500, -300 };

        int finalBalance = ProcessTransactions(initialBalance, transactions);

        Console.WriteLine("Final Balance = " + finalBalance);
    }

    static int ProcessTransactions(int initialBalance, int[] transactions)
    {
        int balance = initialBalance;

        foreach (int transaction in transactions)
        {
            if (transaction >= 0)
            {
                balance += transaction;
            }
            else
            {
                if (balance + transaction >= 0)
                {
                    balance += transaction;
                }
            }
        }

        return balance;
    }
}
