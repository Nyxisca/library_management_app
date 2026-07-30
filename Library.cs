using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Management_Application
{
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
}
