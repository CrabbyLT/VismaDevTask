using System.Linq;
using System.Collections.Generic;
using VismaDevTask.Interfaces;
using VismaDevTask.Models;
using VismaDevTask.Repositories;
using VismaDevTask.Requests;
using System.Linq.Dynamic.Core;
using System;

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

            if (filter is not null)
            {
                if (filter.Field.Equals("Taken/Available books"))
                {
                    books = books
                        .AsQueryable()
                        .Where(b => _repository.GetBookReturnStatus(b.Isbn).Returned != filter.Value.ToLower().Equals("taken"));

                    return ToDictionary(books);
                }

                books = books
                    .AsQueryable()
                    .Where($"b => b.{filter.Field} == \"{filter.Value}\"");

                return ToDictionary(books);
            }

            return ToDictionary(books);
        }

        public void RemoveBookFromLibrary(string isbn)
        {
            var book = _repository.GetBooksFromLibrary().FirstOrDefault(model => model.Isbn.Equals(isbn));

            if (book is null)
            {
                throw new Exception($"There is no book found with ISBN of {book.Isbn}");
            }

            _repository.DeleteBookFromLibraryDatabase(isbn);
        }

        public bool ReturnBookToLibrary(string isbn)
        {
            var currentStatus = _repository.GetBookReturnStatus(isbn);

            if (currentStatus.Returned)
            {
                throw new Exception("Cannot return already returned book!");
            }

            if ((currentStatus.DateTaken + currentStatus.DurationTaken).AddDays(3) > DateTime.Now)
            {
                return false;
            }

            _repository.ReturnBookToLibraryDatabase(isbn);

            return true;
        }

        public void TakeBookFromLibrary(TakeBookRequest bookRequest)
        {
            var book = _repository.GetBooksFromLibrary().FirstOrDefault(model => model.Isbn.Equals(bookRequest.Isbn));

            if (book is null)
            {
                throw new Exception($"There is no book found with ISBN of {book.Isbn}");
            }

            if (_repository.GetBookReturnStatuses().Count(status => status.TakenBy.Equals(bookRequest.TakenBy)) > 3)
            {
                throw new Exception("Can't take more than three books");
            }

            if (bookRequest.TakenDuration.TotalDays > 61)
            {
                throw new Exception("Can't take the book for more than two months");
            }

            _repository.TakeBookFromLibraryDatabase(bookRequest);
        }

        private Dictionary<BookModel, bool> ToDictionary(IEnumerable<BookModel> books)
        {
            Dictionary<BookModel, bool> booksWithStatus = new();
            foreach (var book in books)
            {
                booksWithStatus.Add(book, _repository.GetBookReturnStatus(book.Isbn).Returned);
            }

            return booksWithStatus;
        }
    }
}
