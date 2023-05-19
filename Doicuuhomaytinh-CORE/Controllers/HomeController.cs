using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Doicuuhomaytinh_CORE.Models;
using Doicuuhomaytinh_CORE.ModelViews;
namespace Doicuuhomaytinh_CORE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuanLyDCHMTContext _context;

        public HomeController(ILogger<HomeController> logger, QuanLyDCHMTContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewVM model = new HomeViewVM();
			var Tintuc = _context.News
				   .AsNoTracking()
                   .Include(n => n.Cate)
                   .Where(x => x.Acive == true )
                   .Take(5)
				   .ToList();
			model.News = Tintuc;
			return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}