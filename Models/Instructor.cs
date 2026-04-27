using System.ComponentModel.DataAnnotations;

namespace StudentInfoSystem.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        [Display(Name = "Employee ID")]
        public string EmployeeId { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Specialization { get; set; }

        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; } = DateTime.Now;

        // Foreign key
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        // Navigation properties
        public Department? Department { get; set; }
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}
