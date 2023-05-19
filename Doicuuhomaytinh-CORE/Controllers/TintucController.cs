using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Doicuuhomaytinh_CORE.Helpper;
using Doicuuhomaytinh_CORE.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Doicuuhomaytinh_CORE.Controllers
{
    public class TintucController : Controller
    {
        private readonly QuanLyDCHMTContext _context;
        public TintucController(QuanLyDCHMTContext context)
        {
            _context = context;
        }
        [Route("tin-tuc.html", Name = "tin-tuc")]
        public IActionResult Tintuc(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 100;
            var lsNews = _context.News
                .Include(n => n.Account).Include(n => n.Cate)
                .AsNoTracking()
                .OrderBy(x => x.PostId);
            PagedList<News> models = new PagedList<News>(lsNews, pageNumber, pageSize);
            var lsBaivietlienquan = _context.News
                .AsNoTracking()
                .Where(x => x.Acive == true )
                .Take(4)
                .OrderByDescending(x => x.CreateDate).ToList();
            ViewBag.Baivietlienquan = lsBaivietlienquan;
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }
        [Route("/tin-tuc/{Alias}-{id}.html", Name = "TinChiTiet")]
        public IActionResult Details(int id)
        {
            var News = _context.News.AsNoTracking().SingleOrDefault(x => x.PostId == id);
            if (News == null)
            {
                return RedirectToAction("Index");
            }
            var lsBaivietlienquan = _context.News
                 .AsNoTracking()
                 .Include(x =>x.Cate)
                 .Where(x => x.Acive == true && x.PostId != id && x.Cate.CateId == News.CateId)
                 .Take(4)
                 .OrderByDescending(x => x.CreateDate).ToList();
            ViewBag.Baivietlienquan = lsBaivietlienquan;
            return View(News);
            
        }
    }
}
