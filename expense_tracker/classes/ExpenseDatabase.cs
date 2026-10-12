using System.Dynamic;

public class ExpenseDatabase
{
    // Attributes
    List<Expense> expenses = new();

    // Methods
    public List<Expense> GetExpenses()
    {
        return new List<Expense>(expenses);
    }
    public Expense? GetMostExpensive()
    {
        double highest = 0;
        Expense mostExpensive = new Expense();

        // Check if the list is empty
        if (expenses.Count() < 1)
        {
            return null;
        }

        // Search for most expensive
        foreach (Expense expense in expenses)
        {
            if (expense.Amount > highest)
            {
                highest = expense.Amount;
                mostExpensive = expense;
            }
        }
        return mostExpensive;
    }

    public double CalculateTotal()
    {
        double total = 0;
        foreach (Expense expense in expenses)
        {
            total += expense.Amount;
        }
        return total;
    }

    public bool RemoveExpense(string name)
    {
        name.Trim().ToLower();
        foreach (Expense e in expenses)
        {
            if (e.Name == name)
            {
                expenses.Remove(e);
                return true;
            }
        }
        return false;
    }

    public void AddExpense(Expense expense)
    {
        expenses.Add(expense);
    }

    public bool NameExists(string name)
    {
        foreach (Expense e in expenses)
        {
            if (e.Name == null) return false;

            if (name.Trim().ToLower() == e.Name.Trim().ToLower())
            {
                return true;
            }
        }
        return false;
    }

    public List<Expense> GetExpensesByCategory(string category)
    {
        category.Trim().ToLower();
        List<Expense> filteredExpenses = new List<Expense>();

        foreach (Expense expense in expenses)
        {
            if (expense.Category != null && expense.Category.Trim().ToLower() == category)
            {
                filteredExpenses.Add(expense);
            }
        }
        return filteredExpenses;
    }

    public void AddTestData()
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
}