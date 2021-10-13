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
            _libraryTableDatabase = libraryTableDatabaseFile;
            _bookStatusTableDatabase = bookStatusTableDatabaseFile;
        }

        public string AddBookToLibraryDatabase(BookModel book)
        {
            var readData = GetBooksFromLibrary();

            if (readData.Any(x => x.Isbn.Equals(book.Isbn)))
            {
                throw new Exception("There is already a book with the same isbn. Check again");
            }

            readData.ToList().Add(book);
            var jsonString = JsonSerializer.Serialize(book);
            File.WriteAllText(_libraryTableDatabase, jsonString);

            return book.Isbn;
        }

        public bool DeleteBookFromLibraryDatabase(string isbn)
        {
            var readData = GetBooksFromLibrary();
            readData.ToList().RemoveAll(x => x.Isbn.Equals(isbn));
            var jsonString = JsonSerializer.Serialize(readData);
            File.WriteAllText(_libraryTableDatabase, jsonString);

            return true;
        }

        public IEnumerable<BookModel> GetBooksFromLibrary()
        {
            var jsonString = File.ReadAllText(_libraryTableDatabase);
            var result = JsonSerializer.Deserialize<IEnumerable<BookModel>>(jsonString);

            return result;
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
            var jsonString = JsonSerializer.Serialize(status);

            return result is null;
        }

        private BookReturnStatusModel GetBookReturnStatus(string isbn)
        {
            var bookStatus = GetBookReturnStatuses().First(x => x.Isbn.Equals(isbn));

            return new BookReturnStatusModel
            {
                Isbn = bookStatus.Isbn,
                TakenBy = bookStatus.TakenBy,
                DateTaken = bookStatus.DateTaken,
                Returned = bookStatus.Returned
            };
        }

        private BookModel GetBookFromLibrary(string isbn)
        {
            var book = GetBooksFromLibrary().First(book => book.Isbn.Equals(isbn));

            return book;
        }

        private IEnumerable<BookReturnStatusModel> GetBookReturnStatuses()
        {
            var jsonString = File.ReadAllText(_bookStatusTableDatabase);
            var parsedResult = JsonSerializer.Deserialize<IEnumerable<BookReturnStatusModel>>(jsonString);

            return parsedResult;
        }
    }
}
