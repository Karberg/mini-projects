public class ExpenseDatabase
{
    // Attributes
    public Expense expense = new Expense();
    public List<Expense> expenses = new List<Expense>();

    // Constructors




    // Methods
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
            if (expense.amount > highest)
            {
                highest = expense.amount;
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
            total += expense.amount;
        }
        return total;
    }

    public bool RemoveExpense(string name)
    {
        name.Trim().ToLower();
        foreach (Expense e in expenses)
        {
            if (e.name == name)
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
            if (e.name == null) return false;

            if (name.Trim().ToLower() == e.name.Trim().ToLower())
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
            if (expense.category != null && expense.category.Trim().ToLower() == category)
            {
                filteredExpenses.Add(expense);
            }
        }
        return filteredExpenses;
    }
}