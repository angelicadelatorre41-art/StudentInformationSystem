using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Data;
using StudentInfoSystem.Models;

namespace StudentInfoSystem.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext _context;
        public InstructorController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var instructors = await _context.Instructors
                .Include(i => i.Department)
                .Include(i => i.Subjects)
                .ToListAsync();
            return View(instructors);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var instructor = await _context.Instructors
                .Include(i => i.Department)
                .Include(i => i.Subjects)
                .FirstOrDefaultAsync(m => m.InstructorId == id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,EmployeeId,Specialization,HireDate,DepartmentId")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Instructor created successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", instructor.DepartmentId);
            return View(instructor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null) return NotFound();
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", instructor.DepartmentId);
            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstructorId,FirstName,LastName,Email,EmployeeId,Specialization,HireDate,DepartmentId")] Instructor instructor)
        {
            if (id != instructor.InstructorId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Instructor updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Instructors.Any(e => e.InstructorId == id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", instructor.DepartmentId);
            return View(instructor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var instructor = await _context.Instructors
                .Include(i => i.Department)
                .FirstOrDefaultAsync(m => m.InstructorId == id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor != null) _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Instructor deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
