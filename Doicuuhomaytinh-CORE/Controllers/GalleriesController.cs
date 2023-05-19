using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doicuuhomaytinh_CORE.Models;
using PagedList.Core;

namespace Doicuuhomaytinh_CORE.Controllers
{
    public class GalleriesController : Controller
    {
        private readonly QuanLyDCHMTContext _context;

        public GalleriesController(QuanLyDCHMTContext context)
        {
            _context = context;
        }

        [Route("thu-vien.html", Name = "thu-vien")]
        // GET: Galleries
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var lsGallery = _context.Galleries.AsNoTracking().OrderByDescending(x => x.ImageId);
            PagedList<Gallery> models = new PagedList<Gallery>(lsGallery, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Galleries/Details/5
      

        private bool GalleryExists(int id)
        {
          return (_context.Galleries?.Any(e => e.ImageId == id)).GetValueOrDefault();
        }
    }
}
