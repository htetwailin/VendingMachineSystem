using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendingMachineSystem.Data;
using VendingMachineSystem.Models;

namespace VendingMachineSystem.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly VendingMachineDBContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(VendingMachineDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.User != null ? 
                          View(await _context.User.ToListAsync()) :
                          Problem("Entity set 'VendingMachineSystemContext.User'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            List<SelectListItem> rolelist = new List<SelectListItem>();
            foreach (var role in Enum.GetValues(typeof(Common.EnumRole)))
            {
                rolelist.Add(new SelectListItem
                {
                    Value = ((int)role).ToString(),
                    Text = role.ToString()
                });
            }
            ViewBag.RoleList = rolelist;
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,username,roleid,password,phoneno,address")] User user)
        {
            if (ModelState.IsValid)
            {
                string secretKey = _configuration.GetSection("AppSettings:SecretKey").Value;
                string encryptedPassword = Common.EncryptionHelper.Encrypt(user.password, secretKey);
                user.password = encryptedPassword;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            List<SelectListItem> rolelist = new List<SelectListItem>();
            foreach (var role in Enum.GetValues(typeof(Common.EnumRole)))
            {
                rolelist.Add(new SelectListItem
                {
                    Value = ((int)role).ToString(),
                    Text = role.ToString()
                });
            }
            foreach (var role in rolelist)
            {
                if (int.Parse(role.Value) == user.roleid)
                {
                    role.Selected = true;
                }
            }
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.RoleList = rolelist;
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,username,roleid,password,gender,phoneno,address")] User user)
        {
            if (id != user.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'VendingMachineSystemContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.User?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
