using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Doicuuhomaytinh_CORE.Areas.Admin.Models;
using Doicuuhomaytinh_CORE.Extension;
using Doicuuhomaytinh_CORE.Helpper;
using Doicuuhomaytinh_CORE.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Doicuuhomaytinh_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AccountLoginController : Controller
    {
        private readonly QuanLyDCHMTContext _context;
        public INotyfService _notyfService { get; }
        public AccountLoginController(QuanLyDCHMTContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "Login")]
        public IActionResult Login(string? returnUrl = null)
        {
            var taiKhoanID = HttpContext.Session.GetString("AccountId");
            if (taiKhoanID != null) return RedirectToAction("Index", "Home", new { Area = "Admin" });
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "Login")]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Account acc = _context.Accounts
                        .SingleOrDefault(p => p.Email.ToLower() == model.Email.ToLower().Trim());
                    if (acc == null)
                    {
                        ViewBag.Error = "Thông tin đăng nhập chưa chính xác!";
                        return View(model);
                    }

                    string pass = (model.Password + acc.Salt.Trim()).ToMD5();

                    if (acc.Password.Trim() != pass)
                    {
                        ViewBag.Error = "Thông tin đăng nhập chưa chính xác!";
                        return View(model);
                    }
                    acc.LastLogin = DateTime.Now;
                    _context.Update(acc);
                    await _context.SaveChangesAsync();

                    var taiKhoanID = HttpContext.Session.GetString("AccountId");
                    //Identify
                    //Lưu Session MaAcc
                    HttpContext.Session.SetString("AccountId", acc.AccountId.ToString());

                    var userClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, acc.FullName),
                        new Claim(ClaimTypes.Email, acc.Email),
                        new Claim("AcountId", acc.AccountId.ToString()),
                        new Claim("RoleId", acc.RoleId.ToString())
                    };

                    var grandmaIdentify = new ClaimsIdentity(userClaims, "User Identify");
                    var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentify });
                    await HttpContext.SignInAsync(userPrincipal);

                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
            }
            catch
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
        }
        [Route("logout.html", Name = "Logout")]
        public IActionResult AdminLogout()
        {
            try
            {
                HttpContext.SignOutAsync();
                HttpContext.Session.Remove("AccountId");
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
