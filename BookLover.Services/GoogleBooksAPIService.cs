using BookLover.Data;
using BookLover.Models;
using BookLover.Models.BookModels;
using BookLover.Models.GoogleBooksModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Services
{
    public class GoogleBooksAPIService
    {
        private HttpClient _client;      
        public GoogleBooksAPIService()
        {
            _client = new HttpClient();
        }


        public async Task<GoogleBook> GetBookByGoogleId(string id)
        {
            HttpResponseMessage response =  _client.GetAsync($"https://www.googleapis.com/books/v1/volumes/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                GoogleBook book = await response.Content.ReadAsAsync<GoogleBook>();
                return book;
            }
            return null;
        }

       /* public bool AddGoogleBook(GoogleBook model)
        {
            BookService bookService = new BookService(_userId);   
            //Adds author if they arent' alread in DB
            if (!GoogleBooksAuthorIsAdded(model))
            {
                AuthorServices authorService = new AuthorServices(_userId);
                AuthorCreate authorCreate = new AuthorCreate
                {
                    FirstName = model.AuthorFirstName,
                    LastName = model.AuthorLastName,
                    Description = ""
                };                
                bool authorIsAdded = authorService.CreateAuthor(authorCreate);
            }

            Author bookAuthor = _context.Authors.FirstOrDefault(a => a.FirstName == model.AuthorFirstName
                 && a.LastName == model.AuthorLastName);

            BookCreate book = new BookCreate
            {
                Title = model.VolumeInfo.title,
                Genre = model.Genre,
                Description = model.VolumeInfo.description,
                AuthorId = bookAuthor.AuthorId
            };
            return bookService.CreateBook(book);
        }

        //Checks if there is an author in DB with name matching the Google Book model
        public bool GoogleBooksAuthorIsAdded(GoogleBook model)
        {            
            Author bookAuthor = _context.Authors.FirstOrDefault(a => a.FirstName == model.AuthorFirstName
                    && a.LastName == model.AuthorLastName);
            if (bookAuthor == default)
            {
                return false;
            }
            return true;
        }*/
    }
}
