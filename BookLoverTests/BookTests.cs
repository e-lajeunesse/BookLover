using BookLover.Models.BookModels;
using BookLover.Services;
using BookLover.WebAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Web.Http;


namespace BookLoverTests
{
    [TestClass]
    public class BookTests
    {
        private BookService service = new BookService();



        [TestMethod]
        public void PostBookTest()
        {
            
            BookCreate model = new BookCreate
            {
                Title = "Test Book",
                Genre = "Horror",
                Description = "This is a test",
                AuthorId = 1
            };

            Assert.IsTrue(service.CreateBook(model));
        }


    }
}
