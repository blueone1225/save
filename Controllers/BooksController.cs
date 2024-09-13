using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;

namespace MyModel_CodeFirst.Controllers
{
    public class BooksController : Controller
    {
        private readonly GuestBookContext _context;

        public BooksController(GuestBookContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            //2.2.1 改寫Index Action的內容，將留言依新到舊排序
            return View(await _context.Book.OrderByDescending(b=>b.TimeStamp).ThenByDescending(b=>b.GId).ToListAsync());
        }

       
        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FirstOrDefaultAsync(m => m.GId == id);

            //var rebook = await _context.ReBook.Where(m => m.GId == id).ToListAsync();

            //ViewData["rebook"] = rebook;

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //2.5.2 在BooksController內增加刪除回覆留言Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReBook(long id)
        {
           

            var reBook = await _context.ReBook.FindAsync(id);

            if (reBook != null)
            {
                _context.ReBook.Remove(reBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Delete", new {id=reBook.GId});
        }



        private bool BookExists(long id)
        {
            return _context.Book.Any(e => e.GId == id);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("GId,Title,Description,Author,TimeStamp,Photo,ImageType")] Book book)
        {
            if (id != book.GId)
            {
                return NotFound();
            }

            book.TimeStamp = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.GId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        //2.3.2 在BooksController內增加讀取照片的方法
        public async Task<FileContentResult> GetImage(long gid)
        {
            var book = await _context.Book.FindAsync(gid);

            if(book==null)
            {
                return null;
            }

            return File(book.Photo, book.ImageType);
        }

    }
}
