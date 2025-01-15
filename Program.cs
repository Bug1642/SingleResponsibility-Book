using System;
using System.Linq;
using System.Collections.Generic;
namespace SingleResponsibility
{
    public class BookInfo
    {
        public string bookName { get; set; }
        public string bookWriter { get; set; }
        public long bookISBN { get; set; }

        public BookInfo(string bookName, string bookWriter, long bookISBN)
        {
            this.bookName = bookName;
            this.bookWriter = bookWriter;
            this.bookISBN = bookISBN;
        }
    }
    public class Library
    {
        private List<BookInfo> books { get; set; }
        private Inventory inventory;

        public Library()
        {
            books = new List<BookInfo>();
            inventory = new Inventory(books);
        }
        public void AddBook(BookInfo book)
        {
            if (book.bookISBN.ToString().Length == 13)
            {
                books.Add(book);
                inventory.AddBookToInventory(book);
            }
            else 
            {
                Console.WriteLine("Error: Invalid ISBN length. It should be 13 digits.");
            }
        }

        public void RemoveBook(BookInfo book)
        {
            books.Remove(book);
            inventory.RemoveBookFromInventory(book);
        }
        public void DisplayBooks()
        {
            Console.WriteLine("Library Books: ");
            foreach (var book in books)
            {
                Console.WriteLine($"Book Name: {book.bookName}, Writer Name: {book.bookWriter}, ISBN: {book.bookISBN}");
            }
        }
        public void DisplayInventory()
        {
            inventory.DisplayInventory();
        }
    }
        public class Inventory
        {
        private Dictionary<long, int> inventory = new Dictionary<long, int>();
        private List<BookInfo> books;
        public Inventory(List<BookInfo> books)
        {
            this.books = books;
        }
        public void AddBookToInventory(BookInfo book)
            {
                if (inventory.ContainsKey(book.bookISBN))
                {
                    inventory[book.bookISBN]++;
                }
                else 
                { 
                    inventory.Add(book.bookISBN, 1);
                }
            }
            public void RemoveBookFromInventory(BookInfo book)
            {
                if (inventory.ContainsKey(book.bookISBN))
                {
                    if(inventory[book.bookISBN] > 1)
                    {
                        inventory[book.bookISBN]--;
                    }
                    else
                    {
                        inventory.Remove(book.bookISBN);
                    }
                }
            }
            public void DisplayInventory()
            {
            foreach (var item in inventory)
                {
                var book = books.First(b => b.bookISBN == item.Key);
                Console.WriteLine($"Book Name: {book.bookName}, Writer: {book.bookWriter}, ISBN: {item.Key}, Quantity: {item.Value}");
                }
            }

        }
        public class Program
        {
            static void Main(string[] args)
            {
                BookInfo book1 = new BookInfo("1984", "George Orwell", 9780451524935);
                BookInfo book1_2 = new BookInfo("1984", "George Orwell", 9780451524935);
                BookInfo book2 = new BookInfo("To Kill a Mockingbird", "Harper Lee", 9780061120084);
                BookInfo book3 = new BookInfo("The Great Gatsby", "F. Scott Fitzgerald", 9780743273565);
                BookInfo book4 = new BookInfo("Moby-Dick", "Herman Melville", 9781503280786);
                BookInfo book5 = new BookInfo("Pride and Prejudice", "Jane Austen", 9781503290563);

                Library library = new Library();
                library.AddBook(book1);
                library.AddBook(book1_2);
                library.AddBook(book2);
                library.AddBook(book3);
                library.AddBook(book4);
                library.AddBook(book5);

                library.DisplayBooks();
                library.DisplayInventory();
            }
        }
}
