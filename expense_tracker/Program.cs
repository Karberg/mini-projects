#region Initial Statements
bool running = true;
string? input = "";
ExpenseDatabase expenseDb = new ExpenseDatabase();
#endregion

#region Main Program
Console.WriteLine("Expense Tracker\nYou can enter the following commands:");
PrintCommandList();

expenseDb.AddTestData();

while (running)
{
    Console.Write("Please input a command: ");
    Command command = Command.help;
    input = Console.ReadLine();
    Console.WriteLine();

    // Error handling for null input
    if (input is not null)
    {
        input = input.Trim().ToLower(); // Trim whitespace and convert to lowercase

        if (Enum.IsDefined(typeof(Command), input))
        {
            command = (Command)Enum.Parse(typeof(Command), input);
        }
        else
        {
            Console.WriteLine("Please write a valid command.\nUse 'help' to display a list of commands.\n");
            continue;
        }
    }

    if (command == Command.expenses)
    {
        PrintExpenses(expenseDb.GetExpenses());
    }

    // Add a new item to the expenses
    if (command == Command.add)
    {
        string? name = AddName(expenseDb);

        double amount = AddAmount();

        Console.Write("Category: ");
        string? category = Console.ReadLine();

        Expense expense = new Expense(name, amount, category);
        expenseDb.AddExpense(expense);
        PrintExpenses(expenseDb.GetExpenses());
    }

    // Remove an item from the list of expenses
    if (command == Command.remove)
    {
        Console.Write("Name of the expense to be removed: ");
        string? name = Console.ReadLine();

        if (name is not null && expenseDb.RemoveExpense(name) == true)
        {
            Console.WriteLine($"Expense '{name}' removed.");
            PrintExpenses(expenseDb.GetExpenses());
        }
        else Console.WriteLine("The given expense does not exist in database");

    }

    if (command == Command.category)
    {
        if (expenseDb.GetExpenses().Count() < 1)
        {
            Console.WriteLine("\nThe list of expenses have no items. Please add an expense first.");
            Console.WriteLine();
            continue;
        }

        while (true)
        {
            Console.Write("Enter category: ");
            string? categoryInput = Console.ReadLine();

            if (categoryInput == null) continue;

            List<Expense> filteredExpenses = expenseDb.GetExpensesByCategory(categoryInput.Trim().ToLower());

            // Check if the given category has any elements.
            if (filteredExpenses.Count() > 0)
            {
                PrintExpenses(filteredExpenses);
                break;
            }
            Console.WriteLine("\n'" + categoryInput + "' is not an existing category.");
        }


    }

    if (command == Command.total)
    {
        Console.WriteLine($"Total: ${expenseDb.CalculateTotal():F2}");
    }

    if (command == Command.expensive)
    {
        Expense? mostExpensive = expenseDb.GetMostExpensive();

        if (mostExpensive != null)
        {
            Console.WriteLine($"Most Expensive item: {mostExpensive.Name}: ${mostExpensive.Amount:F2} ({mostExpensive.Category})");
        }
        else Console.WriteLine("There are no expenses in the database.");
    }


    if (command == Command.exit)
    {
        return;
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
        Console.WriteLine($"- {expense.Name}: ${expense.Amount:F2} ({expense.Category})");
    }
}

static double AddAmount()
{
    double amount = 0;
    Console.Write("Amount: ");

    while (amount <= 0)
    {
        string? amountInput = Console.ReadLine();

        if (!double.TryParse(amountInput, out amount) || amount <= 0)
        {
            Console.WriteLine("\nInvalid amount format. \nPlease enter a valid number, greater than zero.\n");
            Console.Write("Amount: ");
            continue;
        }
    }

    return amount;
}

static string? AddName(ExpenseDatabase expenseDb)
{
    string? name;

    while (true)
    {
        Console.Write("Name of expense: ");

        name = Console.ReadLine();
        if (name == null) continue;

        if (!expenseDb.NameExists(name))
        {
            return name;
        }
        else
        {
            Console.WriteLine("\nThis name has already been used.");
        }
    }
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