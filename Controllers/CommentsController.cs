using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using client_server.Models;

namespace client_server.Controllers
{
    public class CommentsController : Controller
    {
        private readonly CommentsDBContext _context;

        public CommentsController(CommentsDBContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comments.OrderByDescending(a => a.CommentId).ToListAsync());
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Username,Comment,userSession,Date")] CommentsModel commentsModel)
        {

            System.Diagnostics.Debug.WriteLine(commentsModel);

            if (ModelState.IsValid)
            {
                _context.Add(commentsModel);
                await _context.SaveChangesAsync();

                return Json(new { communicationCode = 1, comment = commentsModel });
            }

            return Json(new { communicationCode = 0, comModel = commentsModel, state = ModelState });
        }


        // POST: Comments/Delete/5/String
        [HttpPost("Comments/Delete/{id}/{session}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string session)
        {
            var commentsModel = await _context.Comments.FindAsync(id);

            if(commentsModel == null)
            {
                return Json(new { communicationCode = 0, comModel = commentsModel});
            }

            if(commentsModel.userSession == session)
            {
                _context.Comments.Remove(commentsModel);
                await _context.SaveChangesAsync();

                return Json(new { communicationCode = 1, comModel = commentsModel });
            }

            return Json(new { communicationCode = 2, comModel = commentsModel, sess = session });
        }

    }
}
