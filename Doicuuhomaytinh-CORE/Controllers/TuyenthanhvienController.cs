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
namespace Doicuuhomaytinh_CORE.Controllers
{
    public class TuyenthanhvienController : Controller
    {
        private readonly QuanLyDCHMTContext _context;
        public TuyenthanhvienController(QuanLyDCHMTContext context)
        {
            _context = context;
        }
        [Route("tuyen-thanh-vien.html", Name = "TuyenThanhVien")]

        public IActionResult Index(int id)
        {
            var addmember = _context.Addmembers.AsNoTracking().SingleOrDefault(x => x.AddmemberId == 4);
            return View(addmember);
        }
    }
}
