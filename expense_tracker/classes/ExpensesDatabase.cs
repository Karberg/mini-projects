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
}