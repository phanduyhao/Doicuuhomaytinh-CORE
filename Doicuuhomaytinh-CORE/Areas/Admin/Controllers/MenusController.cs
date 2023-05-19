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
    public class MenusController : Controller
    {
        private readonly QuanLyDCHMTContext _context;
        public INotyfService _notyfService { get; }
        public MenusController(QuanLyDCHMTContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Menus
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            return View(await _context.Menus.ToListAsync());
        }

        // GET: Admin/Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Admin/Menus/Create
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            return View();
        }

        // POST: Admin/Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuId,MenuTitle,Alias,Active,Levels,ParentId,ControllerName,ActionName,MenuOrder,Position")] Menu menu)
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
                menu.Alias = Utilities.SEOUrl(menu.MenuTitle);
                _context.Add(menu);
                await _context.SaveChangesAsync();
                _notyfService.Success("Thêm mới thành công!");
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Admin/Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuId,MenuTitle,Alias,Active,Levels,ParentId,ControllerName,ActionName,MenuOrder,Position")] Menu menu)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id != menu.MenuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    menu.Alias = Utilities.SEOUrl(menu.MenuTitle);
                    _context.Update(menu);
                    _notyfService.Success("Cập nhật thành công!");

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.MenuId))
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
            return View(menu);
        }

        // GET: Admin/Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Admin/Menus/Delete/5
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
            if (_context.Menus == null)
            {
                return Problem("Entity set 'QuanLyDCHMTContext.Menus'  is null.");
            }
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
            }
                _notyfService.Success("Xóa thành công!");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
          return _context.Menus.Any(e => e.MenuId == id);
        }
    }
}
