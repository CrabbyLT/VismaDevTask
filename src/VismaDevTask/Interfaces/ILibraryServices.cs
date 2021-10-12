using System.Collections.Generic;
using VismaDevTask.Models;
using VismaDevTask.Requests;

namespace VismaDevTask.Interfaces
{
    public interface ILibraryServices
    {
        string AddBookToLibrary(BookModel book);
        BookModel TakeBookFromLibrary(BookModel book);
        bool ReturnBookToLibrary(BookModel book);
        IEnumerable<BookModel> ListBooksInLibrary(FilterRequest filter);
        bool RemoveBookFromLibrary(BookModel book);
    }
}
