public class Book
{
    #region Attributes
    string Title { get; set; }
    string Author { get; set; }
    int ISBN { get; set; }
    bool IsAvailable { get; set; }
    #endregion


    #region Constructors
    public Book(string title, string author, int isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        IsAvailable = true;
    }
    public Book(string title, string author, int isbn, bool isAvailable)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        IsAvailable = isAvailable;
    }
    #endregion


    #region Methods



    #endregion



}