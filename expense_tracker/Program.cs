using System;
using System.Collections.Generic;

Console.Write("Please input a command: ");
string? input = Console.ReadLine();

if (input == "add")
{
    Console.Write("Name of the expense: ");
    string name = Console.ReadLine();
    Console.Write("Amount: ");
    double amount = double.Parse(Console.ReadLine());
    Console.Write("Category: ");
    string category = Console.ReadLine();

    Expense expense = new Expense { name=name, amount=amount, category=category };
    Console.WriteLine(expense.name + " " + expense.amount + " " + expense.category);
}

if (input == "help")
{
    Console.WriteLine("The supported commands are:");
    Console.WriteLine("* add [name] [amount] [category]");
    Console.WriteLine("* remove [name]");
    Console.WriteLine("* expenses");
    Console.WriteLine("* total");
}


class Expense {
    public string name;
    public double amount = 0;
    public string category;
}
