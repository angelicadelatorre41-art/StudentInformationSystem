using System.ComponentModel.DataAnnotations;

namespace StudentInfoSystem.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Subject Name")]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(20)]
        [Display(Name = "Subject Code")]
        public string Code { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(1, 10)]
        public int Units { get; set; }

        [StringLength(20)]
        public string? Schedule { get; set; }

        [StringLength(50)]
        public string? Room { get; set; }

        // Foreign key
        [Display(Name = "Instructor")]
        public int InstructorId { get; set; }

        // Navigation properties
        public Instructor? Instructor { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
