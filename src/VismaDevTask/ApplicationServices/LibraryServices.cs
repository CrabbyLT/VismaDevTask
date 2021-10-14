using System.Linq;
using System.Collections.Generic;
using VismaDevTask.Interfaces;
using VismaDevTask.Models;
using VismaDevTask.Repositories;
using VismaDevTask.Requests;
using System.Linq.Dynamic.Core;

namespace VismaDevTask.ApplicationServices
{
    public class LibraryServices : ILibraryServices
    {
        private ILibraryRepository _repository;

        public LibraryServices()
        {
            _repository = new LibraryRepository();
        }

        public string AddBookToLibrary(BookModel book)
        {
            return _repository.AddBookToLibraryDatabase(book);
        }

        public Dictionary<BookModel, bool> ListBooksInLibrary(FilterRequest filter)
        {
            var books = _repository.GetBooksFromLibrary();
            Dictionary<BookModel, bool> booksWithStatus = new();

            if (filter is not null)
            {
                books = books
                    .AsQueryable()
                    .Where($"b => b.{filter.Field} == \"{filter.Value}\"");
            }

            foreach (var book in books)
            {
                booksWithStatus.Add(book, _repository.GetBookReturnStatus(book.Isbn).Returned);
            }

            return booksWithStatus;
        }

        public void RemoveBookFromLibrary(string isbn)
        {
            _repository.DeleteBookFromLibraryDatabase(isbn);
        }

        public bool ReturnBookToLibrary(string isbn)
        {
            return _repository.ReturnBookToLibraryDatabase(isbn);
        }

        public bool TakeBookFromLibrary(TakeBookRequest bookRequest)
        {
            return _repository.TakeBookFromLibraryDatabase(bookRequest);
        }
    }
}
