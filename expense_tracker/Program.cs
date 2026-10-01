using System;
using System.Collections.Generic;
using System.Data;

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
        PrintExpenses(expenses);
    }

    if (input == "add")
    {
        Console.Write("Name of the expense: ");
        bool originalName = false;
        string? name = "";

        while (!originalName)
        {
            try
            {
                name = Console.ReadLine();

                foreach (Expense expense1 in expenses)
                {
                    if (name == expense1.name)
                    {
                        throw new DuplicateNameException();
                    }
                }

                originalName = true;
            }
            catch (DuplicateNameException)
            {
                Console.WriteLine("\nThis name has already been used.");
                Console.Write("Name of expense: ");
            }


        }



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
        PrintExpenses(expenses);
    }

    if (input == "remove")
    {
        Console.Write("Name of the expense to be removed: ");
        string name = Console.ReadLine();

        foreach (Expense expense in expenses)
        {
            if (expense.name == name)
            {
                expenses.Remove(expense);
            }
        }

        PrintExpenses(expenses);
    }

    if (input == "total")
    {
        double total = 0;
        foreach (Expense expense in expenses)
        {
            total += expense.amount;
        }

        Console.WriteLine($"Total: ${total:F2}");
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

static void PrintExpenses(List<Expense> expenses)
{
    Console.WriteLine("Expenses command executed.");
    foreach (Expense expense in expenses)
    {
        Console.WriteLine($"- {expense.name}: ${expense.amount:F2} ({expense.category})");
    }
}

class Expense
{
    public string name;
    public double amount = 0;
    public string category;
}
