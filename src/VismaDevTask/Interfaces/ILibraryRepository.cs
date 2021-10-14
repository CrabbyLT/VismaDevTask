using System.Collections.Generic;
using VismaDevTask.Models;
using VismaDevTask.Requests;

namespace VismaDevTask.Repositories
{
    public interface ILibraryRepository
    {
        string AddBookToLibraryDatabase(BookModel book);
        bool TakeBookFromLibraryDatabase(TakeBookRequest bookRequest);
        bool ReturnBookToLibraryDatabase(string isbn);
        IEnumerable<BookModel> GetBooksFromLibrary();
        void DeleteBookFromLibraryDatabase(string isbn);
        BookReturnStatusModel GetBookReturnStatus(string isbn);
    }
}
