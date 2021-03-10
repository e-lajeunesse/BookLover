using BookLover.Data;
using BookLover.Models;
using BookLover.Models.BookModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLover.Services
{
    public class AuthorServices
    {
        private readonly Guid _userId;

        public AuthorServices(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateAuthor(AuthorCreate model)
        {
            var entity =
                new Author()
                {
                    
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    AuthorId = model.AuthorId,
                    Description = model.Description
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Authors.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AuthorListItems> GetAuthors()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Authors
                        .Select(
                            a =>
                                new AuthorListItems
                                {
                                    AuthorId = a.AuthorId,
                                    FirstName = a.FirstName,
                                    LastName = a.LastName,
                                    Description = a.Description
                                }
                        );

                return query.ToArray();
            }
        }

        public AuthorDetail GetAuthorById(int authorId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Authors
                        .Single(a => a.AuthorId == authorId);
                return
                    new AuthorDetail
                    {
                        AuthorId = entity.AuthorId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Description = entity.Description,

                    };
            }
        }

        public bool DeleteAuthor(int authorId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Authors
                        .Single(a => a.AuthorId == authorId);

                ctx.Authors.Remove(entity);

                return ctx.SaveChanges() > 0;
            }
        }

        public bool UpdateAuthor(AuthorEdit model)
        {  
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Authors
                    .FirstOrDefault(a => a.AuthorId == model.AuthorId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Description = model.Description;

                return ctx.SaveChanges() > 0;

            }
        }

        public AuthorDetail GetAuthorByFirstName(string firstName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Authors
                        .FirstOrDefault(a => a.FirstName == firstName);
                return
                    new AuthorDetail
                    {
                        AuthorId = entity.AuthorId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Description = entity.Description,
                    };
                
            }
        }

        public AuthorDetail GetAuthorByLastName(string lastName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Authors
                        .FirstOrDefault(a => a.LastName == lastName);
                return
                    new AuthorDetail
                    {
                        AuthorId = entity.AuthorId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Description = entity.Description,
                    };

            }
        }

    }          
}
