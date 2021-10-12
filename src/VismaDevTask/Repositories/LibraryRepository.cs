using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using VismaDevTask.Models;
using VismaDevTask.Requests;

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
            var readData = GetBooksFromLibraryDatabase();
            readData.ToList().Add(book);
            var jsonString = JsonSerializer.Serialize(book);
            File.WriteAllText(_libraryTableDatabase, jsonString);

            return book.Isbn;
        }

        public bool DeleteBookFromLibraryDatabase(string isbn)
        {
            var readData = GetBooksFromLibraryDatabase();
            readData.ToList().RemoveAll(x => x.Isbn.Equals(isbn));
            var jsonString = JsonSerializer.Serialize(readData);
            File.WriteAllText(_libraryTableDatabase, jsonString);

            return true;
        }

        public IEnumerable<BookModel> GetBooksFromLibraryDatabase()
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

        public BookModel TakeBookFromLibraryDatabase(string name, string isbn)
        {
            var readData = GetBookReturnStatus(isbn);
            var result = GetBooksFromLibraryDatabase().First(model => model.Isbn.Equals(isbn));
            var status = new BookReturnStatusModel { TakenBy = name, Isbn = isbn, DateTaken = DateTime.Now, Returned = false };
            var jsonString = JsonSerializer.Serialize(status);

            return result;
        }

        public BookReturnStatusModel GetBookReturnStatus(string isbn)
        {
            var jsonString = File.ReadAllText(_bookStatusTableDatabase);
            var parsedResult = JsonSerializer.Deserialize<IEnumerable<BookReturnStatusModel>>(jsonString).First(x => x.Isbn.Equals(isbn));

            return new BookReturnStatusModel
            {
                Isbn = parsedResult.Isbn,
                TakenBy = parsedResult.TakenBy,
                DateTaken = parsedResult.DateTaken,
                Returned = parsedResult.Returned
            };
        }
    }
}
