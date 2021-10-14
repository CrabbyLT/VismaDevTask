using System.Collections.Generic;
using VismaDevTask.Models;
using VismaDevTask.Requests;

namespace VismaDevTask.Interfaces
{
    public interface ILibraryServices
    {
        string AddBookToLibrary(BookModel book);
        bool TakeBookFromLibrary(TakeBookRequest bookRequest);
        bool ReturnBookToLibrary(string isbn);
        public Dictionary<BookModel, bool> ListBooksInLibrary(FilterRequest filter);
        void RemoveBookFromLibrary(string isbn);
    }
}
