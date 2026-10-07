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
}