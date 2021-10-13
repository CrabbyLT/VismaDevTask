using System;
using System.Collections.Generic;
using VismaDevTask.Interfaces;
using VismaDevTask.Models;
using VismaDevTask.Repositories;
using VismaDevTask.Requests;

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

        public IEnumerable<BookModel> ListBooksInLibrary(FilterRequest filter)
        {
            throw new NotImplementedException();
        }

        public bool RemoveBookFromLibrary(BookModel book)
        {
            throw new NotImplementedException();
        }

        public bool ReturnBookToLibrary(BookModel book)
        {
            throw new NotImplementedException();
        }

        public bool TakeBookFromLibrary(string isbn, string takenBy)
        {
            return _repository.TakeBookFromLibraryDatabase(isbn, takenBy);
        }
    }
}
