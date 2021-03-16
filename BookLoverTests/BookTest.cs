using BookLover.Data;
using BookLover.Models.BookModels;
using BookLover.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BookLoverTests
{
    [TestClass]
    public class BookTest
    {
        private BookService _repo;
        private Book _content;
        private readonly Guid _userId;
        private BookCreate theShine;


        [TestMethod]
        public void AddBooksTest()
        {
            _repo = new BookService();
            BookCreate firstBook = new BookCreate(
                "Pie",
                "dont expect to sleep tonight",
                "Horror",
                1
                );

            bool wasAdded = _repo.CreateBook(firstBook);

            Console.WriteLine(wasAdded);
            Console.WriteLine(firstBook.Title);

            Assert.IsTrue(wasAdded);
        }

    }
}
