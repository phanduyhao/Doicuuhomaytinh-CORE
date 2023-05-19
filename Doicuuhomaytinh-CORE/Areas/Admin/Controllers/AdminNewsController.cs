using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doicuuhomaytinh_CORE.Models;
using Doicuuhomaytinh_CORE.Helpper;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Doicuuhomaytinh_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminNewsController : Controller
    {
        public INotyfService _notyfService { get; }

        private readonly QuanLyDCHMTContext _context;

        public AdminNewsController(QuanLyDCHMTContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminNews
        public async Task<IActionResult> Index()
        {
            var quanLyDCHMTContext = _context.News.Include(n => n.Account).Include(n => n.Cate);
            return View(await quanLyDCHMTContext.ToListAsync());
        }

        // GET: Admin/AdminNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Account)
                .Include(n => n.Cate)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Admin/AdminNews/Create
        public IActionResult Create()
        {
            
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CateId", "CateName");
            return View();
        }

        // POST: Admin/AdminNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,Contents,Thumb,Acive,Metadesc,MetaKey,Alias,CreateDate,ShortContent,Author,Tags,Ishot,IsNewfeed,CateId,AccountId")] News news, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                 if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string imageName = Utilities.SEOUrl(news.Title) + extension;
                    news.Thumb = await Utilities.UploadFile(fThumb, @"news", imageName.ToLower());
                }
                if (string.IsNullOrEmpty(news.Thumb)) news.Thumb = "default.jpg";
                news.Alias = Utilities.SEOUrl(news.Title);
                news.CreateDate = DateTime.Now;
                _context.Add(news);
                _notyfService.Success("Thêm mới thành công");

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var quanLyDCHMTContext = _context.News
               .AsNoTracking()
               .Include(p => p.Cate)
               .OrderByDescending(p => p.PostId);
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", news.AccountId);
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CateId", "CateName", news.CateId);
            return View(news);
        }

        // GET: Admin/AdminNews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", news.AccountId);
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CateId", "CateName", news.CateId);
            return View(news);
        }

        // POST: Admin/AdminNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Contents,Thumb,Acive,Metadesc,MetaKey,Alias,CreateDate,ShortContent,Author,Tags,Ishot,IsNewfeed,CateId,AccountId")] News news, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != news.PostId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string imageName = Utilities.SEOUrl(news.Title) + extension;
                        news.Thumb = await Utilities.UploadFile(fThumb, @"news", imageName.ToLower());
                    }
                    if (string.IsNullOrEmpty(news.Thumb)) news.Thumb = "default.jpg";
                    news.Alias = Utilities.SEOUrl(news.Title);
                    news.CreateDate = DateTime.Now;
                    _context.Update(news);
                    _notyfService.Success("Cập nhật thành công");

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.PostId))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", news.AccountId);
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CateId", "CateName", news.CateId);
            return View(news);
        }

        // GET: Admin/AdminNews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
          
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Account)
                .Include(n => n.Cate)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Admin/AdminNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.News == null)
            {
                return Problem("Entity set 'QuanLyDCHMTContext.News'  is null.");
            }
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }
                _notyfService.Success("Xóa thành công");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
          return (_context.News?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
