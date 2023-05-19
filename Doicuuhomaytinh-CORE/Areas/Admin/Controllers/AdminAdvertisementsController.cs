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
    public class AdminAdvertisementsController : Controller
    {
        public INotyfService _notyfService { get; }

        private readonly QuanLyDCHMTContext _context;

        public AdminAdvertisementsController(QuanLyDCHMTContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminAdvertisements
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            return _context.Advertisements != null ? 
                          View(await _context.Advertisements.ToListAsync()) :
                          Problem("Entity set 'QuanLyDCHMTContext.Advertisements'  is null.");
        }

        // GET: Admin/AdminAdvertisements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id == null || _context.Advertisements == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .FirstOrDefaultAsync(m => m.AdsId == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Admin/AdminAdvertisements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminAdvertisements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdsId,Title,Thumb,ShortDesc,Contents,Active,Ishot,IsNewfeed,MetaKey,Alias")] Advertisement advertisement, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (ModelState.IsValid)
            {
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string imageName = Utilities.SEOUrl(advertisement.Title) + extension;
                    advertisement.Thumb = await Utilities.UploadFile(fThumb, @"Ads", imageName.ToLower());
                }
                if (string.IsNullOrEmpty(advertisement.Thumb)) advertisement.Thumb = "default.jpg";
                _context.Add(advertisement);
                _notyfService.Success("Thêm mới thành công");

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advertisement);
        }

        // GET: Admin/AdminAdvertisements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id == null || _context.Advertisements == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements.FindAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }
            return View(advertisement);
        }

        // POST: Admin/AdminAdvertisements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdsId,Title,Thumb,ShortDesc,Contents,Active,Ishot,IsNewfeed,MetaKey,Alias")] Advertisement advertisement, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id != advertisement.AdsId)
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
                        string imageName = Utilities.SEOUrl(advertisement.Title) + extension;
                        advertisement.Thumb = await Utilities.UploadFile(fThumb, @"Ads", imageName.ToLower());
                    }
                    if (string.IsNullOrEmpty(advertisement.Thumb)) advertisement.Thumb = "default.jpg";
                    _context.Update(advertisement);
                    _notyfService.Success("Cập nhật thành công");

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(advertisement.AdsId))
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
            return View(advertisement);
        }

        // GET: Admin/AdminAdvertisements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id == null || _context.Advertisements == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .FirstOrDefaultAsync(m => m.AdsId == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // POST: Admin/AdminAdvertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (_context.Advertisements == null)
            {
                return Problem("Entity set 'QuanLyDCHMTContext.Advertisements'  is null.");
            }
            var advertisement = await _context.Advertisements.FindAsync(id);
            if (advertisement != null)
            {
                _context.Advertisements.Remove(advertisement);
            }
                _notyfService.Success("Xóa thành công");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisementExists(int id)
        {
          return (_context.Advertisements?.Any(e => e.AdsId == id)).GetValueOrDefault();
        }
    }
}
