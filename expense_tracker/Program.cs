using System;
using System.Collections.Generic;

bool running = true;
string? input = "";
List<Expense> expenses = new List<Expense>();

while (running)
{

    Console.Write("Please input a command: ");
    input = Console.ReadLine();

    // Error handling for null input
    if (input == null)
    {
        Console.WriteLine("Input cannot be null. Please try again.");
        continue;
    }
    input = input?.Trim().ToLower(); // Trim whitespace and convert to lowercase

    if (input == "expenses")
    {
        Console.WriteLine("Expenses command executed.");
        foreach (Expense expense in expenses)
        {
            Console.WriteLine($"- {expense.name}: ${expense.amount:F2} ({expense.category})");
        }
    }

    if (input == "add")
    {
        Console.Write("Name of the expense: ");
        string name = Console.ReadLine();

        double amount = 0;
        Console.Write("Amount: ");

        while (amount == 0)
        {
            string? amountInput = Console.ReadLine();
            try
            {
                amount = double.Parse(amountInput);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid amount format. Please enter a valid number.");
                Console.Write("Amount: ");
            }
        }

        Console.Write("Category: ");
        string category = Console.ReadLine();

        Expense expense = new Expense { name = name, amount = amount, category = category };
        expenses.Add(expense);
        Console.WriteLine($"- {expense.name}: ${expense.amount:F2} ({expense.category})");
    }

    if (input == "help")
    {
        Console.WriteLine("The supported commands are:");
        Console.WriteLine("* add [name] [amount] [category]");
        Console.WriteLine("* remove [name]");
        Console.WriteLine("* expenses");
        Console.WriteLine("* total");
    }

    if (input == "quit" || input == "exit" || input == "q")
    {
        running = false;
    }

}


class Expense
{
    public string name;
    public double amount = 0;
    public string category;
}
