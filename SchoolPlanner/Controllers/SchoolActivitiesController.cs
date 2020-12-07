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
        // this is called by default
        // TODO: I think I'm gonna need this id later
        public async Task<IActionResult> Index()
        {
            // if no rooms redirect to adding rooms
            if (_context.Room.Count() == 0)
            {
                return Redirect("Rooms");
            }
            PlannerData viewModel = new PlannerData();
            viewModel.currentRoom = _context.Room.FirstOrDefault().RoomNumber.ToString();
            viewModel.Rooms = await _context.Room.ToListAsync();
            return View(viewModel);
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
        public IActionResult Create(int? id)
        {
            ViewData["defaultSlot"] = id;
            return View();
        }

        // POST: SchoolActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Room,Group,Class,Slot,Teacher")] SchoolActivity schoolActivity)
        {
            Console.Write(schoolActivity.Id);
            if (ModelState.IsValid)
            {
                _context.Add(schoolActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schoolActivity);
        }

        // GET: SchoolActivities/Edit/5
        // not actual id but slot
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var schoolActivity = await _context.SchoolActivity.FindAsync(id);
            var schoolActivity = await _context.SchoolActivity.Where(i => i.Slot == id).FirstOrDefaultAsync();
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
        public async Task<IActionResult> GetActivities(string id)
        {
            return Json(new
            {
                activities = await _context.SchoolActivity.Where(i => i.Room == id).ToListAsync()
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            return Json(new
            {
                rooms = await _context.Room.ToListAsync(),
                teachers = await _context.Teacher.ToListAsync(),
                classes = await _context.Class.ToListAsync(),
                groups = await _context.Group.ToListAsync()
            });
        }
    }
}
