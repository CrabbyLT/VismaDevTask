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
            // ..\VismaDevTask\src\VismaDevTask
            var rootDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent;

            _libraryTableDatabase = rootDir + "/" + libraryTableDatabaseFile;
            _bookStatusTableDatabase = rootDir + "/" + bookStatusTableDatabaseFile;
        }

        public string AddBookToLibraryDatabase(BookModel book)
        {
            var booksInLibrary = (List<BookModel>)(GetBooksFromLibrary() ?? new List<BookModel>());
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

            booksInLibrary = booksInLibrary.Where(x => x.Isbn != isbn);
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

            return new List<BookModel>();
        }

        public void ReturnBookToLibraryDatabase(string isbn)
        {
            AddBookStatus(new BookReturnStatusModel(isbn, "", true, default, default));
        }

        public void TakeBookFromLibraryDatabase(TakeBookRequest bookRequest)
        {
            AddBookStatus(new BookReturnStatusModel(
                bookRequest.Isbn, bookRequest.TakenBy, false, DateTime.Now, bookRequest.TakenDuration));
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
                foreach (var status in bookStatusesFromDatabase)
                {
                    if (status.Isbn.Equals(model.Isbn))
                    {
                        status.TakenBy = model.TakenBy;
                        status.Returned = model.Returned;
                        status.DateTaken = model.DateTaken;
                        status.DurationTaken = model.DurationTaken;
                    }
                }
            }

            var jsonString = JsonSerializer.Serialize(bookStatusesFromDatabase);
            File.WriteAllText(_bookStatusTableDatabase, jsonString);
        }

        public IEnumerable<BookReturnStatusModel> GetBookReturnStatuses()
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
