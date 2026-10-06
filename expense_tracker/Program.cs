using System.Data;

#region Initial Statements
bool running = true;
string? input = "";
ExpenseDatabase expenseDB = new ExpenseDatabase();
List<Expense> expenses = new List<Expense>();
#endregion

#region Main Program
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

    // Add a new item to the expenses
    if (input == "add")
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
    if (input == "remove")
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

    if (input == "category")
    {
        bool categoryExists = false;

        if (expenses.Count() < 1)
        {
            Console.WriteLine("\nThe list of expenses have items. Please add an expense first.");
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

    if (input == "total")
    {
        double total = 0;
        foreach (Expense expense in expenses)
        {
            total += expense.amount;
        }

        Console.WriteLine($"Total: ${total:F2}");
    }


    if (input == "quit" || input == "exit" || input == "q")
    {
        running = false;
    }

    if (input == "help")
    {
        Console.WriteLine("The supported commands are:");
        Console.WriteLine("* add [name] [amount] [category]");
        Console.WriteLine("* remove [name]");
        Console.WriteLine("* expenses");
        Console.WriteLine("* category");
        Console.WriteLine("* total");
        Console.WriteLine("* quit || q || exit");
        Console.WriteLine();
    }

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
    Console.WriteLine(); // Add an empty line for better readability
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
#endregion