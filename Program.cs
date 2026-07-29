using System.Text.Json;
using System.IO;

string booksFilePath = "library_books.json";
string patronsFilePath = "library_patrons.json";

Library myLibrary = new Library();
List<Patron> allPatron = new List<Patron>();
bool running = true;

var jsonOptions = new JsonSerializerOptions
{
    WriteIndented = true,
    IncludeFields = true
};

if (File.Exists(booksFilePath))
{
    string booksJson = File.ReadAllText(booksFilePath);
    myLibrary.Bookshelf = JsonSerializer.Deserialize<List<Book>>(booksJson, jsonOptions);
    Console.WriteLine(" Existing books inventory successfully loaded from system storage.");
}
else
{
    myLibrary.AddBook(new Book("How To Kill A Mockingbird", "Harper Lee"));
    myLibrary.AddBook(new Book("1984", "George Orwell"));
    myLibrary.AddBook(new Book("Things Fall Apart", "Chinua Achebe"));
}

if (File.Exists(patronsFilePath))
{
    string patronsJson = File.ReadAllText(patronsFilePath);
    allPatron = JsonSerializer.Deserialize<List<Patron>>(patronsJson, jsonOptions);
    Console.WriteLine("📥 Registered patrons database successfully loaded from system storage.");
}
else
{
    allPatron.Add(new Patron("John Doe", 101));
    allPatron.Add(new Patron("Chuwuemeka David", 102));
    allPatron.Add(new PremiumPatron("Francisca Okorie", 103, "Silver"));
}

while (running)
{
    Console.WriteLine("\n==== LIBRARY MANAGEMENT SYSTEM ====\n");
    Console.WriteLine("1. Display all books");
    Console.WriteLine("2. Add a book");
    Console.WriteLine("3. Register a patron");
    Console.WriteLine("4. Display all patrons");
    Console.WriteLine("5. Display all books borrowed by a patron");
    Console.WriteLine("6. Borrow a book");
    Console.WriteLine("7. Return a book");
    Console.WriteLine("8. Find a book");
    Console.WriteLine("9. Exit the Program");
    Console.Write("\nEnter your choice: ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            myLibrary.DisplayBooks();
            break;

        case "2":
            Console.Write("Enter Book Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter Book Author: ");
            string author = Console.ReadLine();

            myLibrary.AddBook(new Book(title, author));
            Console.WriteLine($"Book '{title}' by {author} has been added to the library.");
            break;

        case "3":
            Console.Write("Enter Patron Name: ");
            string patronName = Console.ReadLine();
            Console.Write("Enter Library Card Number: ");
            int LibraryCardNumber = int.Parse(Console.ReadLine());

            allPatron.Add(new Patron(patronName, LibraryCardNumber));
            Console.WriteLine($"Patron '{patronName}' has been registered.");
            break;

        case "4":
            Console.WriteLine("---ALL PATRONS---");
            foreach (Patron patron in allPatron)
            {
                Console.WriteLine($"- {patron.Name} (Card Number: {patron.LibraryCardNumber})");
            }
            break;

        case "5":
            Console.WriteLine("All Patrons");
            foreach (Patron patron in allPatron)
            {
                Console.WriteLine($"Name: {patron.Name}, Library Card Number: {patron.LibraryCardNumber}");
            }
            Console.Write("\nEnter the EXACT name: ");
            string searchName = Console.ReadLine();

            Patron foundPatron5 = null;
            foreach (Patron p in allPatron)
            {
                if (p.Name.Contains(searchName))
                {
                    foundPatron5 = p;
                    break;
                }
            }
            if (foundPatron5 != null)
            {
                foundPatron5.DisplayBorrowedBooks();
            }
            else
            {
                Console.WriteLine($"No patron found with the name '{searchName}'.");
            }
            break;

        case "6":
            Console.Write("Enter the name of the patron who wants to borrow a book: ");
            string patronNameToBorrow = Console.ReadLine();
            Patron foundPatron6 = null;
            foreach (Patron p in allPatron)
            {
                if (p.Name.Contains(patronNameToBorrow))
                {
                    foundPatron6 = p;
                    break;
                }
            }
            if (foundPatron6 != null)
            {
                Console.Write("Enter the title of the book to borrow: ");
                string bookToBorrow = Console.ReadLine();
                Book foundBook6 = null;
                foreach (Book book in myLibrary.Bookshelf)
                {
                    if (book.Title.Contains(bookToBorrow))
                    {
                        foundBook6 = book;
                        break;
                    }
                }
                if (foundBook6 != null)
                {
                    foundPatron6.BorrowBook(foundBook6);
                }
                else
                {
                    Console.WriteLine($"No book found with the title '{bookToBorrow}'.");
                }
            }
            else
            {
                Console.WriteLine($"No patron found with the name '{patronNameToBorrow}'.");
            }
            break;

        case "7":
            Console.Write("Enter the name of the patron who wants to return a book: ");
            string patronNameToReturn = Console.ReadLine();
            Patron foundPatron7 = null;
            foreach (Patron p in allPatron)
            {
                if (p.Name.Contains(patronNameToReturn))
                {
                    foundPatron7 = p;
                    break;
                }
            }
            if (foundPatron7 != null)
            {
                Console.Write("Enter the title of the book to return: ");
                string bookToReturn = Console.ReadLine();
                Book foundBook7 = null;
                foreach (Book book in foundPatron7.BorrowedBooks)
                {
                    if (book.Title.Contains(bookToReturn))
                    {
                        foundBook7 = book;
                        break;
                    }
                }
                if (foundBook7 != null)
                {
                    foundPatron7.ReturnBook(foundBook7);
                }
                else
                {
                    Console.WriteLine($"No book found with the title '{bookToReturn}' in {foundPatron7.Name}'s borrowed books.");
                }
            }
            else
            {
                Console.WriteLine($"No patron found with the name '{patronNameToReturn}'.");
            }
            break;

        case "8":
            Console.WriteLine("Enter the name of the book you want to find: ");
            string bookToFind = Console.ReadLine();
            Book foundBook8 = null;
            foreach (Book book in myLibrary.Bookshelf)
            {
                if (book.Title.Contains(bookToFind))
                {
                    foundBook8 = book;
                    break;
                }
            }
            if (foundBook8 != null)
            {
                if (foundBook8.IsBorrowed)
                {
                    foreach (Patron patron in allPatron)
                    {
                        if (patron.BorrowedBooks.Contains(foundBook8))
                        {
                            Console.WriteLine($"The book '{foundBook8.Title}' is currently borrowed by {patron.Name}.");
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"The book '{foundBook8.Title}' is available in the library.");
                }
            }
            else
            {
                Console.WriteLine($"No book found with the title '{bookToFind}'.");
            }
            break;

        case "9":
            // SAVE DATA ON EXIT
            Console.WriteLine("Saving library database state to disk...");

            string booksJsonString = JsonSerializer.Serialize(myLibrary.Bookshelf, jsonOptions);
            string patronsJsonString = JsonSerializer.Serialize(allPatron, jsonOptions);

            File.WriteAllText(booksFilePath, booksJsonString);
            File.WriteAllText(patronsFilePath, patronsJsonString);

            Console.WriteLine("Data persistent save complete! Exiting program... Goodbye!");
            running = false;
            break;

        default:
            Console.WriteLine("Invalid input. Please enter a number.");
            break;
    }
    Console.WriteLine("\nPress Enter to go back to homepage...");
    Console.ReadLine();
}

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

class PremiumPatron : Patron
{
    public string MembershipLevel { get; set; }
    public PremiumPatron() : base() { }
    public PremiumPatron(string name, int libraryCardNumber, string membershipLevel)
        : base(name, libraryCardNumber)
    {
        MembershipLevel = membershipLevel;
    }
    public void AccessVipLounge()
    {
        Console.WriteLine($"Premium Member {Name} has entered the VIP lounge.");
    }
    public override void BorrowBook(Book targetbook)
    {
        if (!targetbook.IsBorrowed)
        {
            targetbook.IsBorrowed = true;
            BorrowedBooks.Add(targetbook);
            Console.WriteLine($"Premium Member {Name} has borrowed '{targetbook.Title}'.");
        }
        else
        {
            Console.WriteLine($"Sorry, '{targetbook.Title}' is already borrowed.");
        }
    }
}

class Library
{
    public List<Book> Bookshelf { get; set; }
    public Library()
    {
        Bookshelf = new List<Book>();
    }
    public void AddBook(Book newBook)
    {
        Bookshelf.Add(newBook);
    }
    public void DisplayBooks()
    {
        Console.WriteLine("\n---LIBRARY BOOKS---");
        foreach (Book book in Bookshelf)
        {
            book.DisplayDetails();
        }
    }
}