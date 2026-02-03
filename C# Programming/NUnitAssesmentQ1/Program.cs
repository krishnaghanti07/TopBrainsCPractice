// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using NUnit.Framework;
using System;

[TestFixture]
public class UnitTest
{
    [Test]
    public void Test_Deposit_ValidAmount()
    {
        Program account = new Program(100);
        account.Deposit(50);
        Assert.AreEqual(150, account.Balance);
    }

    [Test]
    public void Test_Deposit_NegativeAmount()
    {
        Program account = new Program(100);
        Exception ex = Assert.Throws<Exception>(() => account.Deposit(-20));
        Assert.AreEqual("Deposit amount cannot be negative", ex.Message);
    }

    [Test]
    public void Test_Withdraw_ValidAmount()
    {
        Program account = new Program(200);
        account.Withdraw(100);
        Assert.AreEqual(100, account.Balance);
    }

    [Test]
    public void Test_Withdraw_InsufficientFunds()
    {
        Program account = new Program(50);
        Exception ex = Assert.Throws<Exception>(() => account.Withdraw(100));
        Assert.AreEqual("Insufficient funds.", ex.Message);
    }
}