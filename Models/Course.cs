using System.ComponentModel.DataAnnotations;

namespace StudentInfoSystem.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Course Name")]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(20)]
        [Display(Name = "Course Code")]
        public string Code { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(1, 10)]
        public int Units { get; set; }

        // Foreign key
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        // Navigation properties
        public Department? Department { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
