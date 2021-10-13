using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using VismaDevTask.ApplicationServices;
using VismaDevTask.Interfaces;
using VismaDevTask.Models;

namespace VismaDevTask.Handlers
{
    public static class InputHandler
    {
        private static ILibraryServices _libraryServices = new LibraryServices();

        public static void Handle(string input)
        {
            switch (input.ToLower())
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
                Console.WriteLine($"{command}   {description}");
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
            Console.WriteLine("Book's publication date (yyyy-mm-dd): ");
            var pubDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
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

            _libraryServices.TakeBookFromLibrary(isbn, takenBy);
        }

        private static void ReturnBook() 
        {
            throw new NotImplementedException();
        }

        private static void ListBooks() 
        {
            throw new NotImplementedException();
        }

        private static void DeleteBook() 
        {
            throw new NotImplementedException();
        }
    }
}
