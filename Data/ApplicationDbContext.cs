using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Models;

namespace StudentInfoSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Department → Course (1-to-many)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Department → Instructor (1-to-many)
            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Department)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course → Student (1-to-many)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Instructor → Subject (1-to-many)
            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Instructor)
                .WithMany(i => i.Subjects)
                .HasForeignKey(s => s.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student → Enrollment (1-to-many)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Subject → Enrollment (1-to-many)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Subject)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, Name = "Computer Science", Code = "CS", Description = "Department of Computer Science and Technology" },
                new Department { DepartmentId = 2, Name = "Information Technology", Code = "IT", Description = "Department of Information Technology" },
                new Department { DepartmentId = 3, Name = "Engineering", Code = "ENG", Description = "Department of Engineering" }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, Name = "Bachelor of Science in Computer Science", Code = "BSCS", Units = 4, DepartmentId = 1 },
                new Course { CourseId = 2, Name = "Bachelor of Science in Information Technology", Code = "BSIT", Units = 4, DepartmentId = 2 },
                new Course { CourseId = 3, Name = "Bachelor of Science in Computer Engineering", Code = "BSCpE", Units = 4, DepartmentId = 3 }
            );

            modelBuilder.Entity<Instructor>().HasData(
                new Instructor { InstructorId = 1, FirstName = "Maria", LastName = "Santos", Email = "m.santos@school.edu", EmployeeId = "EMP001", Specialization = "Software Engineering", HireDate = new DateTime(2018, 6, 1), DepartmentId = 1 },
                new Instructor { InstructorId = 2, FirstName = "Jose", LastName = "Reyes", Email = "j.reyes@school.edu", EmployeeId = "EMP002", Specialization = "Database Systems", HireDate = new DateTime(2019, 8, 15), DepartmentId = 2 },
                new Instructor { InstructorId = 3, FirstName = "Ana", LastName = "Cruz", Email = "a.cruz@school.edu", EmployeeId = "EMP003", Specialization = "Web Development", HireDate = new DateTime(2020, 1, 10), DepartmentId = 1 }
            );

            modelBuilder.Entity<Subject>().HasData(
                new Subject { SubjectId = 1, Name = "Data Structures and Algorithms", Code = "CS101", Units = 3, Schedule = "MWF 8:00-9:00", Room = "Room 201", InstructorId = 1 },
                new Subject { SubjectId = 2, Name = "Database Management Systems", Code = "IT201", Units = 3, Schedule = "TTH 9:00-10:30", Room = "Room 302", InstructorId = 2 },
                new Subject { SubjectId = 3, Name = "Web Development", Code = "CS301", Units = 3, Schedule = "MWF 10:00-11:00", Room = "Lab 1", InstructorId = 3 }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, FirstName = "Juan", LastName = "Dela Cruz", StudentNumber = "2024-00001", Email = "juan@student.edu", Gender = "Male", EnrollmentDate = new DateTime(2024, 8, 1), CourseId = 1 },
                new Student { StudentId = 2, FirstName = "Maria", LastName = "Reyes", StudentNumber = "2024-00002", Email = "maria@student.edu", Gender = "Female", EnrollmentDate = new DateTime(2024, 8, 1), CourseId = 2 },
                new Student { StudentId = 3, FirstName = "Pedro", LastName = "Garcia", StudentNumber = "2024-00003", Email = "pedro@student.edu", Gender = "Male", EnrollmentDate = new DateTime(2024, 8, 1), CourseId = 1 }
            );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { EnrollmentId = 1, StudentId = 1, SubjectId = 1, EnrollmentDate = new DateTime(2024, 8, 5), Status = "Enrolled" },
                new Enrollment { EnrollmentId = 2, StudentId = 1, SubjectId = 3, EnrollmentDate = new DateTime(2024, 8, 5), Status = "Enrolled" },
                new Enrollment { EnrollmentId = 3, StudentId = 2, SubjectId = 2, EnrollmentDate = new DateTime(2024, 8, 5), Status = "Enrolled" },
                new Enrollment { EnrollmentId = 4, StudentId = 3, SubjectId = 1, EnrollmentDate = new DateTime(2024, 8, 5), Status = "Enrolled" }
            );
        }
    }
}
