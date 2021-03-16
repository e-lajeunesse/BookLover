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
            HttpResponseMessage response = _client.GetAsync($"https://www.googleapis.com/books/v1/volumes/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                GoogleBook book = await response.Content.ReadAsAsync<GoogleBook>();
                return book;
            }
            return null;
        }

        public async Task<SearchResult> SearchByTitleAndAuthor(string title, string authorName)
        {
            HttpResponseMessage response = _client.GetAsync(
                $"https://www.googleapis.com/books/v1/volumes?q=intitle:{title}+inauthor:{authorName}" +
                $"&langRestrict=en&printType=books").Result;

            if (response.IsSuccessStatusCode)
            {
                SearchResult result = await response.Content.ReadAsAsync<SearchResult>();
                return result;
            }
            return null;
        }

        public async Task<SearchResult> SearchBooksByAuthor(string authorName)
        {
            HttpResponseMessage response = _client.GetAsync(
                $"https://www.googleapis.com/books/v1/volumes?q=inauthor:\"{authorName}\"&langRestrict=en&projection=lite" +
                $"&maxResults=30&printType=books").Result;

            if (response.IsSuccessStatusCode)
            {
                SearchResult result = await response.Content.ReadAsAsync<SearchResult>();
                return result;
            }
            return null;
        }

        public List<GoogleBook> GetBooksByAuthor(string authorName)
        {
            SearchResult result = SearchBooksByAuthor(authorName).Result;

            if (result.items.Count > 0)
            {
                List<GoogleBook> authorsBooks = new List<GoogleBook>();
                foreach(searchItem item in result.items)
                {
                    GoogleBook book = GetBookByGoogleId(item.id).Result;
                    authorsBooks.Add(book);
                }
                return authorsBooks;
            }
            return null;
        }

        public GoogleBook GetBookByTitleAndAuthor(string title, string authorName)
        {
            SearchResult searchResult = SearchByTitleAndAuthor(title, authorName).Result;

            if (searchResult.items.Count > 0)
            {
                string bookId = searchResult.items[0].id;
                GoogleBook book = GetBookByGoogleId(bookId).Result;
                return book;
            }
            return null;
        }

    }
}
