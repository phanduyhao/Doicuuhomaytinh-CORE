using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doicuuhomaytinh_CORE.Models;

namespace Doicuuhomaytinh_CORE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAddmembersController : Controller
    {
        private readonly QuanLyDCHMTContext _context;

        public AdminAddmembersController(QuanLyDCHMTContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminAddmembers
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            return _context.Addmembers != null ? 
                          View(await _context.Addmembers.ToListAsync()) :
                          Problem("Entity set 'QuanLyDCHMTContext.Addmembers'  is null.");
        }

        // GET: Admin/AdminAddmembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id == null || _context.Addmembers == null)
            {
                return NotFound();
            }

            var addmember = await _context.Addmembers
                .FirstOrDefaultAsync(m => m.AddmemberId == id);
            if (addmember == null)
            {
                return NotFound();
            }

            return View(addmember);
        }

        // GET: Admin/AdminAddmembers/Create
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

        // POST: Admin/AdminAddmembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddmemberId,CreateDate,Contents")] Addmember addmember)
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
                addmember.CreateDate = DateTime.Now;
                _context.Add(addmember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addmember);
        }

        // GET: Admin/AdminAddmembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id == null || _context.Addmembers == null)
            {
                return NotFound();
            }

            var addmember = await _context.Addmembers.FindAsync(id);
            if (addmember == null)
            {
                return NotFound();
            }
            return View(addmember);
        }

        // POST: Admin/TestAddmembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddmemberId,CreateDate,Contents")] Addmember addmember)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id != addmember.AddmemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    addmember.CreateDate = DateTime.Now;
                    _context.Update(addmember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddmemberExists(addmember.AddmemberId))
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
            return View(addmember);
        }

        // GET: Admin/AdminAddmembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            }
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID == null)
                return RedirectToAction("Login", "AccountLogin", new { Area = "Admin" });
            if (id == null || _context.Addmembers == null)
            {
                return NotFound();
            }

            var addmember = await _context.Addmembers
                .FirstOrDefaultAsync(m => m.AddmemberId == id);
            if (addmember == null)
            {
                return NotFound();
            }

            return View(addmember);
        }

        // POST: Admin/AdminAddmembers/Delete/5
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
            if (_context.Addmembers == null)
            {
                return Problem("Entity set 'QuanLyDCHMTContext.Addmembers'  is null.");
            }
            var addmember = await _context.Addmembers.FindAsync(id);
            if (addmember != null)
            {
                _context.Addmembers.Remove(addmember);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddmemberExists(int id)
        {
          return (_context.Addmembers?.Any(e => e.AddmemberId == id)).GetValueOrDefault();
        }
    }
}
