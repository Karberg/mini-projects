public class Expense
{
    #region Attributes
    public string? name;
    public double amount = 0;
    public string? category;
    #endregion

    #region Constructors
    public Expense() { }
    public Expense(string? name, double amount, string? category)
    {
        this.name = name;
        this.amount = amount;
        this.category = category;
    }
    #endregion

    #region Methods
    // Method for adding a name, which only allows original names
    // Access the ExpensesDatabase



    #endregion
}