using BookLover.Models.CommentModels;
using BookLover.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookLover.WebAPI.Controllers
{
    public class CommentController : ApiController
    {
        [Authorize]
        private CommentService CreateCommentService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            CommentService service = new CommentService(userId);
            return service;
        }

        [HttpPost]
        public IHttpActionResult PostComment(CommentCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CommentService service = CreateCommentService();
            if (!service.CreateComment(model))
            {
                return InternalServerError();
            }
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetAllComments()
        {
            CommentService service = CreateCommentService();
            var comments = service.GetAllComments();
            return Ok(comments);
        }
    }
}
