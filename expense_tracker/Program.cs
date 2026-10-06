using System.Data;

#region Initial Statements
bool running = true;
string? input = "";
ExpenseDatabase expenseDb = new ExpenseDatabase();
List<Expense> expenses = new List<Expense>();
#endregion

#region Main Program
Console.WriteLine("Expense Tracker\nYou can enter the following commands:");
PrintCommandList();


while (running)
{
    Console.Write("Please input a command: ");
    Command command = Command.help;
    input = Console.ReadLine();
    Console.WriteLine();

    // Error handling for null input
    if (input is not null && !Enum.IsDefined(typeof(Command), input))
    {
        Console.WriteLine("Please write a valid command.\nUse 'help' to display a list of commands");
        continue;
    }

    if (input is not null)
    {
        input = input.Trim().ToLower(); // Trim whitespace and convert to lowercase

        if (Enum.IsDefined(typeof(Command), input))
        {
            command = (Command)Enum.Parse(typeof(Command), input);
        }
        else
        {
            command = Command.help;
        }
    }

    if (command == Command.expenses)
    {
        PrintExpenses(expenses);
    }

    // Add a new item to the expenses
    if (command == Command.add)
    {
        string? name = AddName(expenses);

        double amount = AddAmount();

        Console.Write("Category: ");
        string? category = Console.ReadLine();

        Expense expense = new Expense(name, amount, category);
        expenses.Add(expense);
        PrintExpenses(expenses);
    }

    // Remove an item from the list of expenses
    if (command == Command.remove)
    {
        Console.Write("Name of the expense to be removed: ");
        string? name = Console.ReadLine();

        foreach (Expense e in expenses)
        {
            if (e.name == name)
            {
                expenses.Remove(e);
                Console.WriteLine($"Expense '{name}' removed.");
                break;
            }
        }

        PrintExpenses(expenses);
    }

    if (command == Command.category)
    {
        bool categoryExists = false;

        if (expenses.Count() < 1)
        {
            Console.WriteLine("\nThe list of expenses have items. Please add an expense first.");
            Console.WriteLine();
            continue;
        }

        while (!categoryExists)
        {

            Console.Write("Enter category: ");
            string? categoryInput = Console.ReadLine();

            // Check each item in expenses and print only the corresponding category
            foreach (Expense expense in expenses)
            {
                if (expense.category == categoryInput)
                {
                    categoryExists = true;
                }
            }

            if (categoryExists)
            {
                Console.WriteLine("\nExpenses in category: " + categoryInput);
                double total = 0;
                foreach (Expense expense in expenses)
                {
                    if (expense.category == categoryInput)
                    {
                        Console.WriteLine($"- {expense.name}: ${expense.amount:F2} ({expense.category})");
                        total += expense.amount;
                    }
                }
                Console.WriteLine($"Total: ${total:F2}");
                Console.WriteLine(); // Add an empty line for better readability
            }

            if (!categoryExists)
            {
                Console.WriteLine("\n'" + categoryInput + "' is not an existing category.");
            }

        }


    }

    if (command == Command.total)
    {
        double total = 0;
        foreach (Expense expense in expenses)
        {
            total += expense.amount;
        }

        Console.WriteLine($"Total: ${total:F2}");
    }


    if (command == Command.exit)
    {
        running = false;
    }

    if (input == "help")
    {
        Console.WriteLine("The supported commands are:");
        PrintCommandList();
    }


    Console.WriteLine();

}
#endregion

#region Helper methods
static void PrintExpenses(List<Expense> expenses)
{
    Console.WriteLine("\nExpenses:");
    foreach (Expense expense in expenses)
    {
        Console.WriteLine($"- {expense.name}: ${expense.amount:F2} ({expense.category})");
    }
}



static double AddAmount()
{
    double amount = 0;
    Console.Write("Amount: ");

    while (amount == 0)
    {
        string? amountInput = Console.ReadLine();

        if (!double.TryParse(amountInput, out amount))
        {
            Console.WriteLine("Invalid amount format. Please enter a valid number.");
            Console.Write("Amount: ");
            continue;
        }
    }

    return amount;
}

static string? AddName(List<Expense> expenses)
{
    Console.Write("Name of the expense: ");
    bool originalName = false;
    string? name = "";

    while (!originalName)
    {
        try
        {
            name = Console.ReadLine();

            foreach (Expense e in expenses)
            {
                if (name == e.name)
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

    return name;
}

static void PrintCommandList()
{
    Console.WriteLine($"1. [{Command.add}] - Add a new expense");
    Console.WriteLine($"2. [{Command.remove}] - Remove an expense");
    Console.WriteLine($"3. [{Command.expenses}] - Show all expenses");
    Console.WriteLine($"4. [{Command.category}] - Show expenses in a given category");
    Console.WriteLine($"5. [{Command.total}] - Calculate the total of all expenses");
    Console.WriteLine($"6. [{Command.expensive}] - Find the most expensive item");
    Console.WriteLine($"0. [{Command.exit}]");
}
#endregion