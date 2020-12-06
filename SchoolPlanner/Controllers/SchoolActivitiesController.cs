using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SchoolPlanner.Models;

namespace SchoolPlanner.Controllers
{
    public class SchoolActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SchoolActivities
        public async Task<IActionResult> Index()
        {
            var viewModel = new PlannerData();
            viewModel.Rooms = await _context.Room.ToListAsync();
            if (viewModel.Rooms.Count() >= 1)
            {
                string currentRoom = viewModel.Rooms.First().RoomNumber.ToString();
                viewModel.SchoolActivities = await _context.SchoolActivity.Where(
                    i => i.Room == currentRoom).ToListAsync();
                return View(viewModel);
            }

            return Redirect("Room/Index");            
        }

        // GET: SchoolActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolActivity = await _context.SchoolActivity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolActivity == null)
            {
                return NotFound();
            }

            return View(schoolActivity);
        }

        // GET: SchoolActivities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SchoolActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Room,Group,Class,Slot,Teacher")] SchoolActivity schoolActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schoolActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schoolActivity);
        }

        // GET: SchoolActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolActivity = await _context.SchoolActivity.FindAsync(id);
            if (schoolActivity == null)
            {
                return NotFound();
            }
            return View(schoolActivity);
        }

        // POST: SchoolActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Room,Group,Class,Slot,Teacher")] SchoolActivity schoolActivity)
        {
            if (id != schoolActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolActivityExists(schoolActivity.Id))
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
            return View(schoolActivity);
        }

        // GET: SchoolActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolActivity = await _context.SchoolActivity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolActivity == null)
            {
                return NotFound();
            }

            return View(schoolActivity);
        }

        // POST: SchoolActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schoolActivity = await _context.SchoolActivity.FindAsync(id);
            _context.SchoolActivity.Remove(schoolActivity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolActivityExists(int id)
        {
            return _context.SchoolActivity.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            return Json(new { data = await _context.Room.ToListAsync() });
        }
    }
}
