using Moq;
using NUnit.Framework;
using System;
using VismaDevTask.Interfaces;
using VismaDevTask.ApplicationServices;
using VismaDevTask.Models;
using VismaDevTask.Repositories;
using System.Collections.Generic;
using System.Linq;

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
            _libraryServices = new LibraryServices();
            _libraryRepositoryMock = new Mock<ILibraryRepository>();
            _book = new BookModel("A story about a killer flower", "A. Johson", "Murder", "English", new DateTime(2020, 08, 21), "46546-5464-2321-45423");
        }

        [Test]
        public void Given__bookInfo_When_AddBookToLibrary_Then_Returns_booksIsbn()
        {
            // TODO: Later
            Assert.That(false);
        }

        [Test]
        public void Given_BookInfo_When_RemoveBookFromLibrary_Then_ReturnsTrue()
        {
            // TODO: Later
            Assert.That(false);
        }

        [Test]
        public void Given_FilterRequest_When_GetBooksFromLibraryDatabase_Then_Returns_FilteredList()
        {
            // TODO: Later
            Assert.That(false);
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