using System.Collections.Generic;
using VismaDevTask.Models;
using VismaDevTask.Requests;

namespace VismaDevTask.Repositories
{
    public interface ILibraryRepository
    {
        string AddBookToLibraryDatabase(BookModel book);
        BookModel TakeBookFromLibraryDatabase(string isbn, string name);
        bool ReturnBookToLibraryDatabase(string isbn);
        IEnumerable<BookModel> GetBooksFromLibraryDatabase();
        bool DeleteBookFromLibraryDatabase(string isbn);
    }
}
