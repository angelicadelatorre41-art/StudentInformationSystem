using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Data;
using StudentInfoSystem.Models;

namespace StudentInfoSystem.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject).ThenInclude(s => s!.Instructor)
                .ToListAsync();
            return View(enrollments);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var enrollment = await _context.Enrollments
                .Include(e => e.Student).ThenInclude(s => s!.Course)
                .Include(e => e.Subject).ThenInclude(s => s!.Instructor)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null) return NotFound();
            return View(enrollment);
        }

        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentDate,Status,Grade,LetterGrade,StudentId,SubjectId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Enrollment created successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName", enrollment.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name", enrollment.SubjectId);
            return View(enrollment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null) return NotFound();
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName", enrollment.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name", enrollment.SubjectId);
            return View(enrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentId,EnrollmentDate,Status,Grade,LetterGrade,StudentId,SubjectId")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Enrollment updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Enrollments.Any(e => e.EnrollmentId == id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName", enrollment.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name", enrollment.SubjectId);
            return View(enrollment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null) return NotFound();
            return View(enrollment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null) _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Enrollment deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
