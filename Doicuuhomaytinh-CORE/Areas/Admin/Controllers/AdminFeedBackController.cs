using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Doicuuhomaytinh_CORE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Doicuuhomaytinh_CORE.Helpper;
using Doicuuhomaytinh_CORE.Models;

namespace Doicuuhomaytinh_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminFeedBackController : Controller
    {
        private readonly QuanLyDCHMTContext _context;

        public AdminFeedBackController(QuanLyDCHMTContext context)
        {
            _context = context;
        }

        // GET: Admin/FeedBacks
        public IActionResult Index(int? page)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var Pagesize = 20;
            var IsNews = _context.FeedBacks.AsNoTracking().OrderByDescending(x => x.FeedbackId);
            PagedList<FeedBack> models = new PagedList<FeedBack>(IsNews, pageNumber, Pagesize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FeedBacks == null)
            {
                return NotFound();
            }

            var feedBack = await _context.FeedBacks.FindAsync(id);
            if (feedBack == null)
            {
                return NotFound();
            }
            return View(feedBack);
        }

        // POST: Admin/FeedBacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeedbackId,Fullname,Email,Contents,Active,Title")] FeedBack feedBack)
        {
            if (id != feedBack.FeedbackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedBack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedBackExists(feedBack.FeedbackId))
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
            return View(feedBack);
        }

        // GET: Admin/FeedBacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id == null || _context.FeedBacks == null)
            {
                return NotFound();
            }

            var feedBack = await _context.FeedBacks
                .FirstOrDefaultAsync(m => m.FeedbackId == id);
            if (feedBack == null)
            {
                return NotFound();
            }

            return View(feedBack);
        }

        // GET: Admin/FeedBacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FeedBacks == null)
            {
                return NotFound();
            }

            var feedBack = await _context.FeedBacks
                .FirstOrDefaultAsync(m => m.FeedbackId == id);
            if (feedBack == null)
            {
                return NotFound();
            }

            return View(feedBack);
        }

        // POST: Admin/FeedBacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FeedBacks == null)
            {
                return Problem("Entity set 'Shop2HandContext.FeedBacks'  is null.");
            }
            var feedBack = await _context.FeedBacks.FindAsync(id);
            if (feedBack != null)
            {
                _context.FeedBacks.Remove(feedBack);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedBackExists(int id)
        {
            return _context.FeedBacks.Any(e => e.FeedbackId == id);
        }
    }
}
