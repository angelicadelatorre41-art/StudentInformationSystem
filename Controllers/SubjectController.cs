using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Data;
using StudentInfoSystem.Models;

namespace StudentInfoSystem.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SubjectController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var subjects = await _context.Subjects
                .Include(s => s.Instructor)
                .Include(s => s.Enrollments)
                .ToListAsync();
            return View(subjects);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var subject = await _context.Subjects
                .Include(s => s.Instructor)
                .Include(s => s.Enrollments).ThenInclude(e => e.Student)
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null) return NotFound();
            return View(subject);
        }

        public IActionResult Create()
        {
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "InstructorId", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Code,Description,Units,Schedule,Room,InstructorId")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Subject created successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "InstructorId", "FullName", subject.InstructorId);
            return View(subject);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null) return NotFound();
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "InstructorId", "FullName", subject.InstructorId);
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectId,Name,Code,Description,Units,Schedule,Room,InstructorId")] Subject subject)
        {
            if (id != subject.SubjectId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Subject updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Subjects.Any(e => e.SubjectId == id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "InstructorId", "FullName", subject.InstructorId);
            return View(subject);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var subject = await _context.Subjects
                .Include(s => s.Instructor)
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null) return NotFound();
            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null) _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Subject deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
