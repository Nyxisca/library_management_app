namespace Library_Management_Application
{
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsBorrowed { get; set; }
        public Book() { }

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
            IsBorrowed = false;
        }

        public void DisplayDetails()
        {
            string status = IsBorrowed ? "Borrowed" : "Available";
            Console.WriteLine($"'{Title}' by {Author} - Status: {status}");
        }
    }
}
