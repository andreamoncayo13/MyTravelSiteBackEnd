using myTravelAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myTravelAPI.Models;
using Microsoft.Identity.Client;
using System.ComponentModel.Design;

namespace myTravelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public CommentController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var comments = await _dbContext.Comments.ToListAsync();

            return Ok(comments);

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int CommentId)
        {
            var comment = await _dbContext.Comments.FindAsync(CommentId);
            if (comment == null)
                return NotFound(new { Message = "Comment Not Found!" });
            else {
                _dbContext.Comments.Remove(comment);
                _dbContext.SaveChanges();

                return Ok(new
                {
                    Message = "Successful Delete"
                });
            }
        }


        [HttpPost ("create")]
       public async Task<IActionResult> CreateComment(Comment comment)
       {
                _dbContext.Comments.Add(comment);
           await _dbContext.SaveChangesAsync();

            //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
           return CreatedAtAction(nameof(Comment), new { comment });
        }
    }
}
