using System;
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
                    ListHelpCommands();
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
            throw new NotImplementedException();
        }

        private static void ListHelpCommands() 
        {
            throw new NotImplementedException();
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
