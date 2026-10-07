using System.Data;

#region Initial Statements
bool running = true;
string? input = "";
ExpenseDatabase expenseDb = new ExpenseDatabase();
#endregion

#region Main Program
Console.WriteLine("Expense Tracker\nYou can enter the following commands:");
PrintCommandList();

AddTestData(expenseDb.expenses);

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
        PrintExpenses(expenseDb.expenses);
    }

    // Add a new item to the expenses
    if (command == Command.add)
    {
        string? name = AddName(expenseDb.expenses);

        double amount = AddAmount();

        Console.Write("Category: ");
        string? category = Console.ReadLine();

        Expense expense = new Expense(name, amount, category);
        expenseDb.expenses.Add(expense);
        PrintExpenses(expenseDb.expenses);
    }

    // Remove an item from the list of expenses
    if (command == Command.remove)
    {
        Console.Write("Name of the expense to be removed: ");
        string? name = Console.ReadLine();

        foreach (Expense e in expenseDb.expenses)
        {
            if (e.name == name)
            {
                expenseDb.expenses.Remove(e);
                Console.WriteLine($"Expense '{name}' removed.");
                break;
            }
        }

        PrintExpenses(expenseDb.expenses);
    }

    if (command == Command.category)
    {
        bool categoryExists = false;

        if (expenseDb.expenses.Count() < 1)
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
            foreach (Expense expense in expenseDb.expenses)
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
                foreach (Expense expense in expenseDb.expenses)
                {
                    if (expense.category == categoryInput)
                    {
                        Console.WriteLine($"- {expense.name}: ${expense.amount:F2} ({expense.category})");
                        total += expense.amount;
                    }
                }
                Console.WriteLine($"Total: ${total:F2}");
            }

            if (!categoryExists)
            {
                Console.WriteLine("\n'" + categoryInput + "' is not an existing category.");
            }

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
            Console.WriteLine($"Most Expensive item: {mostExpensive.name}: ${mostExpensive.amount:F2} ({mostExpensive.category})");
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

static void AddTestData(List<Expense> expenses)
{
    expenses.AddRange(new[]
    {
        new Expense("Banana", 20, "Fruit"),
        new Expense("Apple", 15, "Fruit"),
        new Expense("Milk", 18, "Groceries"),
        new Expense("Bread", 25, "Groceries"),
        new Expense("Chicken", 65, "Groceries"),
        new Expense("Coffee", 42, "Food & Drinks"),
        new Expense("Lunch", 85, "Food & Drinks"),
        new Expense("Pizza", 110, "Food & Drinks"),

        new Expense("Bus Ticket", 24, "Transport"),
        new Expense("Train Ticket", 95, "Transport"),
        new Expense("Gas", 450, "Transport"),
        new Expense("Parking", 35, "Transport"),

        new Expense("Netflix", 99, "Subscriptions"),
        new Expense("Spotify", 109, "Subscriptions"),

        new Expense("T-Shirt", 200, "Clothing"),
        new Expense("Shoes", 750, "Clothing"),

        new Expense("Cinema", 130, "Entertainment"),
        new Expense("Video Game", 450, "Entertainment"),

        new Expense("Electricity", 550, "Bills"),
        new Expense("Internet", 299, "Bills"),
        new Expense("Phone Bill", 149, "Bills"),
        new Expense("Rent", 5200, "Bills"),

        new Expense("Gym Membership", 250, "Health"),
        new Expense("Medicine", 85, "Health"),

        new Expense("Notebook", 35, "School"),
        new Expense("Textbook", 450, "School"),

        new Expense("Birthday Gift", 300, "Gifts")
    });
}

#endregion