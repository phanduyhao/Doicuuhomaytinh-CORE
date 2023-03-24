/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Doicuuhomaytinh_CORE.Models;
using Doicuuhomaytinh_CORE.Helpper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Doicuuhomaytinh_CORE.Controllers
{
    public class RegisterController : Controller

    {
        public INotyfService _notyfService { get; }
        private readonly QuanLyDCHMTContext _Context;
        public RegisterController(QuanLyDCHMTContext context, INotyfService notyfService)
        {
            _notyfService = notyfService;
            _Context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Account user)
        {
            if (user == null)
            {
                return NotFound();
            }
            var check = _Context.Accounts.Where(m => m.Email == user.Email).FirstOrDefault();
            if (check != null)
            {
                Functions._MessageEmail = "Email đã tồn tại!";
                return RedirectToRoute("dang-ky.html");
            }
            else
            {
                Functions._MessageEmail = string.Empty;
                _Context.Add(user);
                _Context.SaveChanges();
            }
            return RedirectToRoute("dang-nhap.html");
        }
    }
}
*/