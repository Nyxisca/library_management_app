namespace Library_Management_Application
{
    class Patron
    {
        public string Name { get; set; }
        public int LibraryCardNumber { get; set; }
        public List<Book> BorrowedBooks { get; set; }
        public Patron()
        {
            BorrowedBooks = new List<Book>();
        }
        public Patron(string name, int libraryCardNumber)
        {
            Name = name;
            LibraryCardNumber = libraryCardNumber;
            BorrowedBooks = new List<Book>();
        }
        public virtual void BorrowBook(Book targetbook)
        {
            if (!targetbook.IsBorrowed)
            {
                targetbook.IsBorrowed = true;
                BorrowedBooks.Add(targetbook);
                Console.WriteLine($"{Name} has borrowed '{targetbook.Title}'.");
            }
            else
            {
                Console.WriteLine($"Sorry, '{targetbook.Title}' is already borrowed.");
            }
        }
        public void ReturnBook(Book targetbook)
        {
            if (BorrowedBooks.Contains(targetbook))
            {
                targetbook.IsBorrowed = false;
                BorrowedBooks.Remove(targetbook);
                Console.WriteLine($"{Name} has returned '{targetbook.Title}'.");
            }
            else
            {
                Console.WriteLine($"'{targetbook.Title}' was not borrowed.");
            }
        }
        public void DisplayBorrowedBooks()
        {
            Console.WriteLine($"---{Name.ToUpper()}'S BORROWED BOOKS---:");
            if (BorrowedBooks.Count == 0)
            {
                Console.WriteLine("No books currently borrowed.");
            }
            else
            {
                foreach (Book book in BorrowedBooks)
                {
                    Console.WriteLine($"- {book.Title} by {book.Author}");
                }
            }
        }
    }
}
