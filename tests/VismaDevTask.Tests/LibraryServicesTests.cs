using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using VismaDevTask.ApplicationServices;
using VismaDevTask.Interfaces;
using VismaDevTask.Models;
using VismaDevTask.Repositories;
using VismaDevTask.Requests;

namespace VismaDevTask.Tests
{
    public class LibraryServicesTests
    {
        private ILibraryServices _libraryServices;
        private Mock<ILibraryRepository> _libraryRepositoryMock;
        private BookModel _book;

        [SetUp]
        public void Setup()
        {
            _libraryRepositoryMock = new Mock<ILibraryRepository>();
            _libraryServices = new LibraryServices(_libraryRepositoryMock.Object);

            _book = new BookModel(
                "A story about a killer flower", 
                "A. Johson", 
                "Detective", 
                "English", 
                new DateTime(2020, 08, 21),
                "123-456-6531-1");
        }

        [Test]
        public void Given_NewBookISBN_When_AddBookToLibrary_Then_ReturnsBooksIsbn()
        {
            _libraryRepositoryMock.Setup(repository => repository.AddBookToLibraryDatabase(_book)).Returns(_book.Isbn);

            var result = _libraryServices.AddBookToLibrary(_book);

            _libraryRepositoryMock.Verify(mock => mock.AddBookToLibraryDatabase(It.IsAny<BookModel>()), Times.Once());
            Assert.That(result.Equals(_book.Isbn));
        }

        [Test]
        public void Given_ExistingBookISBN_When_AddBookToLibrary_Then_Throws()
        {
            _libraryRepositoryMock.Setup(repository => repository.GetBooksFromLibrary()).Returns(new List<BookModel>() { _book });

            _libraryRepositoryMock.Verify(mock => mock.AddBookToLibraryDatabase(It.IsAny<BookModel>()), Times.Never());
            Assert.That(
                () => _libraryServices.AddBookToLibrary(_book), 
                Throws.TypeOf<Exception>().With.Message.EqualTo("Book with the same ISBN already exists in the book."));
        }

        [Test]
        public void Given_BookISBN_When_RemoveBookFromLibrary_Then_ThrowsNothing()
        {
            _libraryRepositoryMock.Setup(repository => repository.GetBooksFromLibrary()).Returns(new List<BookModel>() { _book });

            Assert.That(() => _libraryServices.RemoveBookFromLibrary(_book.Isbn), !Throws.TypeOf<Exception>());
        }

        [Test]
        public void Given_NonExistingBookISBN_When_RemoveBookFromLibrary_Then_Throws()
        {
            _libraryRepositoryMock.Setup(repository => repository.GetBooksFromLibrary());

            Assert.That(
                () => _libraryServices.RemoveBookFromLibrary(_book.Isbn),
                Throws.TypeOf<Exception>().With.Message.EqualTo($"There is no book found with ISBN of {_book.Isbn}"));
        }

        [Test]
        public void Given_FilterRequestForDetectiveCategory_When_GetBooksFromLibraryDatabase_Then_Returns_FilteredList()
        {
            _libraryRepositoryMock.Setup(repository => repository.GetBooksFromLibrary())
                .Returns(new List<BookModel> 
                { 
                    _book, 
                    new BookModel("A book", "A. Autor", "Mystery", "English", new DateTime(2021, 07, 06), "123-566-578-8"),
                    new BookModel("Another book about murder", "S. Ander", "Detective", "English", new DateTime(2000,01,06), "321-664-987-3-33")
                });
            _libraryRepositoryMock.Setup(x => x.GetBookReturnStatus(_book.Isbn)).Returns(new BookReturnStatusModel(_book.Isbn, "Unit tester", false, DateTime.Now, TimeSpan.FromDays(3)));
            _libraryRepositoryMock.Setup(x => x.GetBookReturnStatus("123-566-578-8")).Returns(new BookReturnStatusModel("123-566-578-8", "", true, default, default));
            _libraryRepositoryMock.Setup(x => x.GetBookReturnStatus("321-664-987-3-33")).Returns(new BookReturnStatusModel("321-664-987-3-33", "", true, default, default));

            var actual = _libraryServices.ListBooksInLibrary(new FilterRequest("Category", "Detective"));

            Assert.That(actual.Count() == _libraryRepositoryMock.Object.GetBooksFromLibrary().Where(x => x.Category.Equals("Detective")).Count());
        }

        [Test]
        public void Given_FilterRequestForAvailableBooks_When_GetBooksFromLibraryDatabase_Then_Returns_FilteredList()
        {
            _libraryRepositoryMock.Setup(repository => repository.GetBooksFromLibrary())
                .Returns(new List<BookModel>
                {
                    _book,
                    new BookModel("A book", "A. Autor", "Mystery", "English", new DateTime(2021, 07, 06), "123-566-578-8"),
                    new BookModel("Another book about murder", "S. Ander", "Detective", "English", new DateTime(2000,01,06), "321-664-987-3-33")
                });
            _libraryRepositoryMock.Setup(x => x.GetBookReturnStatus(_book.Isbn)).Returns(new BookReturnStatusModel(_book.Isbn, "Unit tester", false, DateTime.Now, TimeSpan.FromDays(3)));
            _libraryRepositoryMock.Setup(x => x.GetBookReturnStatus("123-566-578-8")).Returns(new BookReturnStatusModel("123-566-578-8", "", true, default, default));
            _libraryRepositoryMock.Setup(x => x.GetBookReturnStatus("321-664-987-3-33")).Returns(new BookReturnStatusModel("321-664-987-3-33", "", true, default, default));

            var actual = _libraryServices.ListBooksInLibrary(new FilterRequest("Taken/Available books", "Available"));

            Assert.That(actual.Count() == _libraryRepositoryMock.Object.GetBooksFromLibrary().Where(x => x.Category.Equals("Detective")).Count());
        }

        [Test]
        public void Given_NoFilterRequest_When_GetBooksFromLibraryDatabase_Then_Returns_UnfilteredList()
        {
            _libraryRepositoryMock.Setup(repository => repository.GetBooksFromLibrary())
                .Returns(new List<BookModel>
                {
                    _book,
                    new BookModel("A book", "A. Autor", "Mystery", "English", new DateTime(2021, 07, 06), "123-566-578-8"),
                    new BookModel("Another book about murder", "S. Ander", "Detective", "English", new DateTime(2000,01,06), "321-664-987-3-33")
                });
            _libraryRepositoryMock.Setup(x => x.GetBookReturnStatus(_book.Isbn)).Returns(new BookReturnStatusModel(_book.Isbn, "Unit tester", false, DateTime.Now, TimeSpan.FromDays(3)));
            _libraryRepositoryMock.Setup(x => x.GetBookReturnStatus("123-566-578-8")).Returns(new BookReturnStatusModel("123-566-578-8", "", true, default, default));
            _libraryRepositoryMock.Setup(x => x.GetBookReturnStatus("321-664-987-3-33")).Returns(new BookReturnStatusModel("321-664-987-3-33", "", true, default, default));

            var actual = _libraryServices.ListBooksInLibrary(null);

            Assert.That(actual.Count() == _libraryRepositoryMock.Object.GetBooksFromLibrary().Count());
        }

        [Test]
        public void Given_NameAndIsbn_When_TakeBookFromLibrary_Then_Returns_True()
        {
            // TODO: Later
            Assert.That(false);
        }

        [Test]
        public void Given_ReturnBookOnTime_WhenReturnBookToLibrary_Then_Returns_SuccessMessage()
        {
            // TODO: Later
            Assert.That(false);
        }

        [Test]
        public void Given_ReturnBookNotOnTime_WhenReturnBookToLibrary_Then_Returns_FunnyMessage()
        {
            // TODO: Later
            Assert.That(false);
        }
    }
}