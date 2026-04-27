using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Data;

namespace StudentInfoSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.StudentCount = await _context.Students.CountAsync();
            ViewBag.CourseCount = await _context.Courses.CountAsync();
            ViewBag.DepartmentCount = await _context.Departments.CountAsync();
            ViewBag.InstructorCount = await _context.Instructors.CountAsync();
            ViewBag.SubjectCount = await _context.Subjects.CountAsync();
            ViewBag.EnrollmentCount = await _context.Enrollments.CountAsync();
            return View();
        }
    }
}
