public class Expense
{
    #region Attributes
    public string? Name { get; set; }
    public double Amount { get; set; }
    public string? Category { get; set; }
    #endregion

    #region Constructors
    public Expense() { }
    public Expense(string? name, double amount, string? category)
    {
        Name = name;
        Amount = amount;
        Category = category;
    }
    #endregion

    #region Methods
    // Method for adding a name, which only allows original names
    // Access the ExpensesDatabase



    #endregion
}