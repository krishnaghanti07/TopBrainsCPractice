// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartBankApp
{
    public abstract class BankAccount
    {
        public int AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public double Balance { get; set; }
        public List<string> Transactions { get; set; }

        public BankAccount(int accNo, string name, double bal)
        {
            AccountNumber = accNo;
            CustomerName = name;
            Balance = bal;
            Transactions = new List<string>();
        }

        public virtual void Deposit(double amount)
        {
            if (amount <= 0)
                throw new InvalidTransactionException("Invalid deposit amount");

            Balance += amount;
            Transactions.Add("Deposited: " + amount);
        }

        public virtual void Withdraw(double amount)
        {
            if (amount <= 0)
                throw new InvalidTransactionException("Invalid withdraw amount");

            if (amount > Balance)
                throw new InsufficientBalanceException("Not enough balance");

            Balance -= amount;
            Transactions.Add("Withdrawn: " + amount);
        }

        public abstract double CalculateInterest();

        public void ShowDetails()
        {
            Console.WriteLine(AccountNumber + " - " + CustomerName + " - Balance: " + Balance);
        }
    }

    public class SavingsAccount : BankAccount
    {
        public double MinBalance { get; set; } = 1000;

        public SavingsAccount(int accNo, string name, double bal) : base(accNo, name, bal) { }

        public override void Withdraw(double amount)
        {
            if (Balance - amount < MinBalance)
                throw new MinimumBalanceException("Minimum balance required");

            base.Withdraw(amount);
        }

        public override double CalculateInterest()
        {
            return Balance * 0.04;
        }
    }

    public class CurrentAccount : BankAccount
    {
        public double OverdraftLimit { get; set; } = 20000;

        public CurrentAccount(int accNo, string name, double bal) : base(accNo, name, bal) { }

        public override void Withdraw(double amount)
        {
            if (amount > Balance + OverdraftLimit)
                throw new InsufficientBalanceException("Overdraft limit exceeded");

            Balance -= amount;
            Transactions.Add("Withdrawn: " + amount);
        }

        public override double CalculateInterest()
        {
            return 0;
        }
    }

    public class LoanAccount : BankAccount
    {
        public LoanAccount(int accNo, string name, double bal) : base(accNo, name, bal) { }

        public override void Deposit(double amount)
        {
            throw new InvalidTransactionException("Deposit not allowed in loan account");
        }

        public override double CalculateInterest()
        {
            return Balance * 0.1;
        }
    }

    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string msg) : base(msg) { }
    }

    public class MinimumBalanceException : Exception
    {
        public MinimumBalanceException(string msg) : base(msg) { }
    }

    public class InvalidTransactionException : Exception
    {
        public InvalidTransactionException(string msg) : base(msg) { }
    }

    class Program
    {
        static void Transfer(BankAccount fromAcc, BankAccount toAcc, double amount)
        {
            fromAcc.Withdraw(amount);
            toAcc.Deposit(amount);
            fromAcc.Transactions.Add("Transferred to " + toAcc.AccountNumber);
            toAcc.Transactions.Add("Received from " + fromAcc.AccountNumber);
        }

        static void Main(string[] args)
        {
            List<BankAccount> accounts = new List<BankAccount>();

            accounts.Add(new SavingsAccount(101, "Rahul", 60000));
            accounts.Add(new CurrentAccount(102, "Ramesh", 40000));
            accounts.Add(new LoanAccount(103, "Amit", 80000));
            accounts.Add(new SavingsAccount(104, "Ritika", 30000));
            accounts.Add(new CurrentAccount(105, "Sohan", 90000));

            int choice = 0;

            while (choice != 6)
            {
                Console.WriteLine("\n1. Show All Accounts");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. LINQ Reports");
                Console.WriteLine("6. Exit");
                Console.Write("Enter choice: ");
                choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    if (choice == 1)
                    {
                        foreach (var acc in accounts)
                            acc.ShowDetails();
                    }
                    else if (choice == 2)
                    {
                        Console.Write("Account No: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Amount: ");
                        double amt = Convert.ToDouble(Console.ReadLine());

                        var acc = accounts.FirstOrDefault(a => a.AccountNumber == id);
                        acc?.Deposit(amt);
                    }
                    else if (choice == 3)
                    {
                        Console.Write("Account No: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Amount: ");
                        double amt = Convert.ToDouble(Console.ReadLine());

                        var acc = accounts.FirstOrDefault(a => a.AccountNumber == id);
                        acc?.Withdraw(amt);
                    }
                    else if (choice == 4)
                    {
                        Console.Write("From Account: ");
                        int fromId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("To Account: ");
                        int toId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Amount: ");
                        double amt = Convert.ToDouble(Console.ReadLine());

                        var fromAcc = accounts.FirstOrDefault(a => a.AccountNumber == fromId);
                        var toAcc = accounts.FirstOrDefault(a => a.AccountNumber == toId);

                        if (fromAcc != null && toAcc != null)
                            Transfer(fromAcc, toAcc, amt);
                    }
                    else if (choice == 5)
                    {
                        Console.WriteLine("\nBalance > 50000:");
                        var rich = accounts.Where(a => a.Balance > 50000);
                        foreach (var a in rich)
                            a.ShowDetails();

                        Console.WriteLine("\nTotal Bank Balance:");
                        Console.WriteLine(accounts.Sum(a => a.Balance));

                        Console.WriteLine("\nTop 3 Highest Balance:");
                        var top = accounts.OrderByDescending(a => a.Balance).Take(3);
                        foreach (var a in top)
                            a.ShowDetails();

                        Console.WriteLine("\nGrouped By Type:");
                        var groups = accounts.GroupBy(a => a.GetType().Name);
                        foreach (var g in groups)
                        {
                            Console.WriteLine(g.Key);
                            foreach (var a in g)
                                a.ShowDetails();
                        }

                        Console.WriteLine("\nCustomers starting with R:");
                        var names = accounts.Where(a => a.CustomerName.StartsWith("R"));
                        foreach (var a in names)
                            a.ShowDetails();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
