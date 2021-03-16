using BookLover.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookLover.Models.GoogleBooksModels
{
    public class GoogleBook
    {
        public string Id { get; set; }
        public VolumeInfo VolumeInfo { get; set; }

        public string Description
        {
            get
            {
                if (VolumeInfo.Description != null)
                {

                    return RemoveHTML(VolumeInfo.Description);
                }
                return null;
            }
        }
        
        private string[] AuthorName
        {
            get
            {
                char[] separator = { ' ' };
                return VolumeInfo.Authors[0].Split(separator, 2);
            }
        }
        public string AuthorFirstName
        {
            get
            {
                return AuthorName[0];
            }
        }
        public string AuthorLastName
        {
            get
            {
                return AuthorName[1];
            }
        }

        public string Genre
        {
            get
            {
                foreach (string genre in Book.ValidGenres)
                {
                    if (Categories.Contains(genre.ToLower()))
                    {
                        return genre;
                    }
                }
                return "Unavailable";
            }
        }


        public List<string> Categories
        {
            get
            {
                if (VolumeInfo.Categories != null)
                {
                    List<string> genres = new List<string>();
                    char[] separators = { '/' };
                    foreach (string s in VolumeInfo.Categories)
                    {
                        string[] cats = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string cat in cats)
                        {
                            string final = cat.ToLower().Trim();
                            genres.Add(final);
                        }
                    }
                    return genres; 
                }
                return new List<string> { "Unavailable" };
            }
        }

        private static string RemoveHTML(string description)
        {
            Regex regex = new Regex("\\<[^\\>]*\\>");
            description = regex.Replace(description, String.Empty);
            return description;
        }
    }
}
