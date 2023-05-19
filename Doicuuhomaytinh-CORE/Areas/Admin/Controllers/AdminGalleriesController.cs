using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doicuuhomaytinh_CORE.Models;
using Doicuuhomaytinh_CORE.Helpper;

namespace Doicuuhomaytinh_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminGalleriesController : Controller
    {
        private readonly QuanLyDCHMTContext _context;

        public AdminGalleriesController(QuanLyDCHMTContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminGalleries2
        public async Task<IActionResult> Index()
        {
            return _context.Galleries != null ?
                        View(await _context.Galleries.ToListAsync()) :
                        Problem("Entity set 'QuanLyDCHMTContext.Galleries'  is null.");
        }

        // GET: Admin/AdminGalleries2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Galleries == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // GET: Admin/AdminGalleries2/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminGalleries2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageId,Image,ImageName")] Gallery gallery, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string imageName = Utilities.SEOUrl(gallery.ImageName) + extension;
                    gallery.ImageName = await Utilities.UploadFile(fThumb, @"gallery", imageName.ToLower());
                }
                if (string.IsNullOrEmpty(gallery.ImageName)) gallery.ImageName = "default.jpg";
                _context.Add(gallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gallery);
        }

        // GET: Admin/AdminGalleries2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Galleries == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return NotFound();
            }
            return View(gallery);
        }

        // POST: Admin/AdminGalleries2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageId,Image,ImageName")] Gallery gallery, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != gallery.ImageId)
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
                        string imageName = Utilities.SEOUrl(gallery.ImageName) + extension;
                        gallery.ImageName = await Utilities.UploadFile(fThumb, @"gallery", imageName.ToLower());
                    }
                    if (string.IsNullOrEmpty(gallery.ImageName)) gallery.ImageName = "default.jpg";
                    _context.Update(gallery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryExists(gallery.ImageId))
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
            return View(gallery);
        }

        // GET: Admin/AdminGalleries2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Galleries == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // POST: Admin/AdminGalleries2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Galleries == null)
            {
                return Problem("Entity set 'QuanLyDCHMTContext.Galleries'  is null.");
            }
            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery != null)
            {
                _context.Galleries.Remove(gallery);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalleryExists(int id)
        {
            return (_context.Galleries?.Any(e => e.ImageId == id)).GetValueOrDefault();
        }
    }
}
