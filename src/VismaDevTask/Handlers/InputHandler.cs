using System;
using System.Collections.Generic;
using System.Globalization;
using VismaDevTask.ApplicationServices;
using VismaDevTask.Interfaces;
using VismaDevTask.Models;
using VismaDevTask.Requests;

namespace VismaDevTask.Handlers
{
    public static class InputHandler
    {
        private static ILibraryServices _libraryServices = new LibraryServices();

        public static void Handle(string input)
        {
            switch (input.ToLower().TrimEnd())
            {
                case "help":
                    ListCommands();
                    break;
                case "add":
                    AddBook();
                    break;
                case "take":
                    TakeBook();
                    break;
                case "return":
                    ReturnBook();
                    break;
                case "list":
                    ListBooks();
                    break;
                case "delete" or "remove" or "del":
                    DeleteBook();
                    break;
                default:
                    Console.WriteLine("Unknown command. Enter 'help' for a list of available commands");
                    break;
            }

            Console.WriteLine("\r\nAwaiting command...");
        }

        private static void ListCommands()
        {
            var helpDictionary = new Dictionary<string, string>()
            {
                { "help", "Displays all the commands with their descriptions." },
                { "add", "Add the book to the library." },
                { "take", "Take the book from the library." },
                { "return", "Return the taken book to the library." },
                { "list", "Lists all the books in the library with all the info you need." },
                { "delete", "Alias: remove, del. Deletes the book from the library." },
                { "exit", "Exit the program." }
            };

            foreach (var (command, description) in helpDictionary)
            {
                Console.WriteLine($"{command}: {description}");
            }
        }

        private static void AddBook()
        {
            Console.WriteLine("Write all the details you want, to add the book");
            Console.WriteLine("Book name: ");
            var name = Console.ReadLine();

            Console.WriteLine("Book's author (F. Surname): ");
            var author = Console.ReadLine();

            Console.WriteLine("Book's category (only one for now): ");
            var category = Console.ReadLine();

            Console.WriteLine("Book's language: ");
            var language = Console.ReadLine();

            Console.WriteLine("Book's publication date (yyyy): ");
            var pubDate = DateTime.ParseExact(Console.ReadLine(), "yyyy", CultureInfo.InvariantCulture);

            Console.WriteLine("Book's ISBN: ");
            var isbn = Console.ReadLine();

            var result = _libraryServices.AddBookToLibrary(new BookModel(name, author, category, language, pubDate, isbn));

            Console.WriteLine($"The book with ISBN of {result} has been added successfully");
        }

        private static void TakeBook()
        {
            Console.WriteLine("I see you want to take a book home, huh?");
            Console.WriteLine("Please enter your name");
            var takenBy = Console.ReadLine();

            Console.WriteLine("Let me scan the ISBN from your book");
            Console.WriteLine("Please enter the ISBN");
            var isbn = Console.ReadLine();

            Console.WriteLine("For how long the book will be taken? (Time duration in days)");
            var timeTaken = Convert.ToDouble(Console.ReadLine());

            _libraryServices.TakeBookFromLibrary(new TakeBookRequest(takenBy, isbn, TimeSpan.FromDays(timeTaken)));

            Console.WriteLine("Successfully taken the book");
        }

        private static void ReturnBook() 
        {
            Console.WriteLine("Returning the book? Let's see");
            Console.WriteLine("Enter the ISBN");
            var isbn = Console.ReadLine();

            var late = _libraryServices.ReturnBookToLibrary(isbn);

            if (late) Console.WriteLine("You are late Sugar Tits! \r\n https://www.youtube.com/watch?v=ZYJBpIC7ndA");
            else Console.WriteLine("Right one time! Hope you had a great read :)");
        }

        private static void ListBooks() 
        {
            Console.WriteLine("Would you like to filter any value? Available options:");

            var options = new string[] { "Author", "Category", "Language", "ISBN", "Name", "Taken/Available books", "None" };

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i+1}: {options[i]}");
            }

            var optionSelected = Convert.ToInt32(Console.ReadLine())-1;
            FilterRequest filterRequest = null;

            if (options[optionSelected].ToLower() != "none")
            {
                Console.WriteLine($"{options[optionSelected]} selected. Please enter value");
                var value = Console.ReadLine();

                filterRequest = new FilterRequest(options[optionSelected], value);
            }

            Console.WriteLine("Listing all the books..");
            Console.WriteLine("Author       | Name                              | Category       | Language     | Publication Date   | ISBN               | Taken/Available |");
            foreach (var (book, available) in (_libraryServices.ListBooksInLibrary(filterRequest)))
            {
                var takenString = available ? "Available" : "Taken";
                Console.WriteLine($"{book.Author,-12} | {book.Name,-33} | {book.Category,-14} | {book.Language,-12} | {book.PublicationDate,-18:yyyy} | {book.Isbn,18} | {takenString,-15} |");
            }
        }

        private static void DeleteBook() 
        {
            Console.WriteLine("Book needs to be removed? Go ahead, tell me the ISBN");
            var isbn = Console.ReadLine();

            _libraryServices.RemoveBookFromLibrary(isbn);

            Console.WriteLine($"The book of ISBN: {isbn} has been successfully removed!");
        }
    }
}
