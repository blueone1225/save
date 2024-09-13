using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;

namespace MyModel_CodeFirst.Controllers
{
    public class PostBooksController : Controller
    {
        private readonly GuestBookContext _context;

        public PostBooksController(GuestBookContext context)
        {
            _context = context;
        }

        //3.1.6 修改Index Action的寫法
        public async Task<IActionResult> Index()
        {
            //var book = await _context.Book.ToListAsync();

            var book = _context.Book.OrderByDescending(b => b.TimeStamp).ThenByDescending(b => b.GId);

            return View(await book.ToListAsync());

            //return View(await _context.Book.ToListAsync());
        }

        // GET: PostBooks/Details/5
        public async Task<IActionResult> Display(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.GId == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: PostBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile? uploadPhoto)
        {
            //上傳照片的處理

            if (uploadPhoto != null)
            {
                if (uploadPhoto.ContentType != "image/jpeg" && uploadPhoto.ContentType != "image/png")
                {
                    ViewData["Message"] = "請上傳jpg或png格式的檔案!!";
                    return View(book);
                }

                //把檔案轉成二進位資料,並放入byte[]
                using (var mem = new MemoryStream())
                {
                    uploadPhoto.CopyTo(mem);

                    book.Photo = mem.ToArray();
                    book.ImageType = uploadPhoto.ContentType;
                }
            }

            book.TimeStamp = DateTime.Now;


            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        //3.1.5 修改PostBooksController，移除Edit、Delete Action

        private bool BookExists(long id)
        {
            return _context.Book.Any(e => e.GId == id);
        }


        public IActionResult ExceptionTest()
        {
            int a = 0;
            int s;

            if (a == 0)
            {
                // 當 a 為 0 時，返回一個包含影片預覽的 HTML 內容
                string url = "https://www.youtube.com/watch?v=LHOVDbuWPB8";
                string videoId = "LHOVDbuWPB8"; // YouTube 影片的 ID

                // YouTube 影片預覽的嵌入代碼
                string htmlContent = $@"
            <iframe width='2000' height='2500' src='https://www.youtube.com/embed/{videoId}' frameborder='0' allow='accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture' allowfullscreen></iframe>
            <br>
            <a href='{url}' target='_blank'>Click here to visit the traffic website</a>
            <br>
            <a href='https://www.youtube.com/watch?v={videoId}' target='_blank'>Black Myth: Wukong</a>";

                return Content(htmlContent, "text/html");
            }
            else
            {
                try
                {
                    // 正常情況下處理除法
                    s = 100 / a;
                    return Content($"Result of division: {s}");
                }
                catch (DivideByZeroException)
                {
                    // 捕獲除以零異常並返回錯誤信息
                    return Content("Error: Division by zero is not allowed.");
                }
            }
        }

    }
}
