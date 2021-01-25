using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using client_server.Models;
using client_server.Data.Models;
using System;

namespace client_server.Controllers
{
  public class CommentsController : Controller
  {
    private readonly ApplicationDBContext _context;

    public CommentsController(ApplicationDBContext context)
    {
      _context = context;
    }

    // GET: Comments
    public async Task<IActionResult> Index()
    {
      return View(await _context.Comments.OrderByDescending(a => a.CommentId).ToListAsync());
    }

    // POST: Comments/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CommentsModel commentsModel)
    {

      if (ModelState.IsValid)
      {
        commentsModel.Date = DateTime.UtcNow;
        _context.Add(commentsModel);
        await _context.SaveChangesAsync();

        return Json(new { communicationCode = 1, comment = commentsModel });
      }

      return Json(new { communicationCode = 0, comModel = commentsModel, state = ModelState });
    }


    // POST: Comments/Delete/
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(CommentsModel model)
    {
      var commentsModel = await _context.Comments.FindAsync(model.CommentId);

      if (commentsModel == null)
      {
        return Json(new { communicationCode = 0, comModel = commentsModel });
      }

      if (commentsModel.UserId == model.UserId)
      {
        _context.Comments.Remove(commentsModel);
        await _context.SaveChangesAsync();

        return Json(new { communicationCode = 1, comModel = commentsModel });
      }

      return Json(new { communicationCode = 2, comModel = commentsModel });
    }

  }
}
