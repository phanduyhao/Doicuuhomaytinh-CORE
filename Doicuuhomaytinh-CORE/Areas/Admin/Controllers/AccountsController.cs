using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doicuuhomaytinh_CORE.Models;
using Microsoft.AspNetCore.Authorization;
using Doicuuhomaytinh_CORE.Areas.Admin.Models;
using System.Security.Claims;
using Doicuuhomaytinh_CORE.Helpper;
using Doicuuhomaytinh_CORE.Extension;
using System.Security.Principal;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Doicuuhomaytinh_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountsController : Controller
    {
        public INotyfService _notyfService { get; }

        private readonly QuanLyDCHMTContext _context;

        public AccountsController(QuanLyDCHMTContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;

        }

        // GET: Admin/Accounts
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });

            return View(await _context.Accounts.ToListAsync());
        }

        // GET: Admin/Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Admin/Accounts/Create
        public IActionResult Create()
        {
            ViewData["PhanQuyen"] = new SelectList(_context.Accounts, "RoleId", "RoleName");

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            return View();
        }

        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,FullName,Email,Phone,Password,Salt,Active,CreateDate,RoleId,LastLogin")] Account account)
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
                string salt = Utilities.GetRandomKey();
                account.Password = (account.Password + salt.Trim()).ToMD5();
                account.Salt = salt;
                account.CreateDate = DateTime.Now;
                if (account.RoleId == 1)
                    account.RoleName = "Admin";
                if (account.RoleId == 2)
                    account.RoleName = "Staff";
                _context.Add(account);
                _notyfService.Success("Thêm mới thành công");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhanQuyen"] = new SelectList(_context.Accounts, "RoleId", "RoleName", account.RoleId);
            return View(account);
        }

        // GET: Admin/Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["PhanQuyen"] = new SelectList(_context.Accounts, "RoleId", "RoleName", account.RoleId);
            return View(account);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,FullName,Email,Phone,Password,Salt,Active,CreateDate,RoleId,LastLogin")] Account account)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (account.Password != null)
                    {
                        string salt = Utilities.GetRandomKey();
                        account.Password = (account.Password + salt.Trim()).ToMD5();
                        account.Salt = salt;
                    }
                    else
                    {
                        account = _context.Accounts.FirstOrDefault(a => a.AccountId == id);
                        string pass = account.Password;
                        string salt = account.Salt;
                        account.Password = pass;
                        account.Salt = salt;
                    }
                    account.CreateDate = DateTime.Now;
                    if (account.RoleId == 1)
                        account.RoleName = "Admin";
                    if (account.RoleId == 2)
                        account.RoleName = "Staff";
                    _context.Update(account);
                    _notyfService.Success("Cập nhật thành công");

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
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

            ViewData["PhanQuyen"] = new SelectList(_context.Accounts, "RoleId", "RoleName", account.RoleId);
            return View(account);
        }

        // GET: Admin/Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Admin/Accounts/Delete/5
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
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'QuanLyDCHMTContext.Accounts'  is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }
            _notyfService.Success("Xóa thành công");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}
