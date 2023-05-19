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
    public class SearchController : Controller
    {
        QuanLyDCHMTContext db;
        public SearchController(QuanLyDCHMTContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult Index(string name)
        {
            var results = from b in db.News select b;
            
            if(!string.IsNullOrEmpty(name) )
            {
                results = results.Where(x => x.Title.Contains(name));
            }
            var lsBaivietlienquan = db.News
                 .AsNoTracking()
                 .Include(x => x.Cate)
                 .Where(x => x.Acive == true && x.Title.Contains(name).Equals(name) == false )
                 .Take(4)
                 .OrderByDescending(x => x.CreateDate).ToList();
            ViewBag.Baivietlienquan = lsBaivietlienquan;
            ViewBag.name = name;
            return View(results);
        }
    }
}
