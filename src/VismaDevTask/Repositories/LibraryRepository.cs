using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using VismaDevTask.Models;

namespace VismaDevTask.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly string _libraryTableDatabase;
        private readonly string _bookStatusTableDatabase;

        public LibraryRepository(string libraryTableDatabaseFile = "LibraryDatabase.json", string bookStatusTableDatabaseFile = "BookStatusDatabase.json")
        {
            // ..\VismaDevTask\src\VismaDevTask
            var rootDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent;

            _libraryTableDatabase = rootDir + "/" + libraryTableDatabaseFile;
            _bookStatusTableDatabase = rootDir + "/" + bookStatusTableDatabaseFile;
        }

        public string AddBookToLibraryDatabase(BookModel book)
        {
            var booksInLibrary = (List<BookModel>)(GetBooksFromLibrary() ?? new List<BookModel>());

            if (booksInLibrary.Any(x => x.Isbn.Equals(book.Isbn)))
            {
                throw new Exception("There is already a book with the same isbn. Check again");
            }

            booksInLibrary.Add(book);
            var jsonString = JsonSerializer.Serialize(booksInLibrary);
            File.WriteAllText(_libraryTableDatabase, jsonString);
            AddBookStatus(new BookReturnStatusModel(book.Isbn, "", true, default, default));

            return book.Isbn;
        }

        public void DeleteBookFromLibraryDatabase(string isbn)
        {
            var booksInLibrary = GetBooksFromLibrary();

            if (booksInLibrary is null)
            {
                throw new Exception("There are no books in the library :(");
            }

            booksInLibrary.ToList().RemoveAll(x => x.Isbn.Equals(isbn));
            var jsonString = JsonSerializer.Serialize(booksInLibrary);
            File.WriteAllText(_libraryTableDatabase, jsonString);
        }

        public IEnumerable<BookModel> GetBooksFromLibrary()
        {
            if (File.Exists(_libraryTableDatabase))
            {
                var jsonString = File.ReadAllText(_libraryTableDatabase);
                var result = JsonSerializer.Deserialize<IEnumerable<BookModel>>(jsonString);

                return result;
            }

            return null;
        }

        public bool ReturnBookToLibraryDatabase(string isbn)
        {
            var status = GetBookReturnStatus(isbn);
            status.Returned = true;
            var jsonString = JsonSerializer.Serialize(status);
            File.WriteAllText(_bookStatusTableDatabase, jsonString);

            return status.DateTaken.Subtract(DateTime.Now).TotalDays <= 3;
        }

        public bool TakeBookFromLibraryDatabase(string name, string isbn)
        {
            var readData = GetBookReturnStatus(isbn);
            var result = GetBooksFromLibrary().First(model => model.Isbn.Equals(isbn));
            var status = new BookReturnStatusModel { TakenBy = name, Isbn = isbn, DateTaken = DateTime.Now, Returned = false };
            AddBookStatus(status);

            return result is null;
        }

        public BookReturnStatusModel GetBookReturnStatus(string isbn)
        {
            var bookStatus = GetBookReturnStatuses().First(status => status.Isbn.Equals(isbn));

            return bookStatus;
        }

        private void AddBookStatus(BookReturnStatusModel model)
        {
            var bookStatusesFromDatabase = (List<BookReturnStatusModel>)GetBookReturnStatuses() ?? new List<BookReturnStatusModel>();

            if (!bookStatusesFromDatabase.Any(status => status.Isbn.Equals(model.Isbn)))
            {
                bookStatusesFromDatabase.Add(model);
            }
            else
            {
                bookStatusesFromDatabase
                    .Where(status => status.Isbn.Equals(model.Isbn))
                    .ToList()
                    .ForEach(status => status = model);
            }

            var jsonString = JsonSerializer.Serialize(bookStatusesFromDatabase);
            File.WriteAllText(_bookStatusTableDatabase, jsonString);
        }

        private IEnumerable<BookReturnStatusModel> GetBookReturnStatuses()
        {
            if (File.Exists(_bookStatusTableDatabase))
            {
                var jsonString = File.ReadAllText(_bookStatusTableDatabase);
                var parsedResult = JsonSerializer.Deserialize<IEnumerable<BookReturnStatusModel>>(jsonString);

                return parsedResult;
            }

            return null;
        }
    }
}
