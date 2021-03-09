﻿using BookLover.Data;
using BookLover.Models;
using System;
using System.Collections.Generic;
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
                    UserId = _userId,
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
                        .Where(a => a.UserId == _userId)
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

        public AuthorDetail GetAuthorById(int AuthorId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Authors
                        .Single(a => a.AuthorId == AuthorId && a.UserId == _userId);
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
