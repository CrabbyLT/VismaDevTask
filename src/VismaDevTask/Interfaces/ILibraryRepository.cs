using System.Collections.Generic;
using VismaDevTask.Models;

namespace VismaDevTask.Repositories
{
    public interface ILibraryRepository
    {
        string AddBookToLibraryDatabase(BookModel book);
        bool TakeBookFromLibraryDatabase(string isbn, string name);
        bool ReturnBookToLibraryDatabase(string isbn);
        IEnumerable<BookModel> GetBooksFromLibrary();
        bool DeleteBookFromLibraryDatabase(string isbn);
    }
}
