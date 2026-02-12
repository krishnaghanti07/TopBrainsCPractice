using System;
using System.Collections.Generic;

public class Program
{
public static Stack<Order> st { get; set; } = new Stack<Order>();

public static void Main(string[] args)
{
Order ord = new Order();

int id = Convert.ToInt32(Console.ReadLine());
string name = Console.ReadLine();
string item = Console.ReadLine();

ord.AddOrderDetails(id, name, item);

Console.WriteLine(ord.GetOrderDetails());

ord.RemoveOrderDetails();
}
}

public class Order
{
public int Id { get; set; }
public string Name { get; set; }
public string Item { get; set; }

public Stack<Order> AddOrderDetails(int id, string name, string item)
{
Order newOrder = new Order
{
Id = id,
Name = name,
Item = item
};

Program.st.Push(newOrder);
return Program.st;
}

public string GetOrderDetails()
{
Order top = Program.st.Peek();
return top.Id + " " + top.Name + " " + top.Item;
}

public Stack<Order> RemoveOrderDetails()
{
Program.st.Pop();
return Program.st;
}
}
