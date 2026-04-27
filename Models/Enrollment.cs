using System.ComponentModel.DataAnnotations;

namespace StudentInfoSystem.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string? Status { get; set; } = "Enrolled";

        [Range(0, 100)]
        [DisplayFormat(NullDisplayText = "No grade yet")]
        public double? Grade { get; set; }

        [StringLength(5)]
        [Display(Name = "Letter Grade")]
        public string? LetterGrade { get; set; }

        // Foreign keys
        [Display(Name = "Student")]
        public int StudentId { get; set; }

        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        // Navigation properties
        public Student? Student { get; set; }
        public Subject? Subject { get; set; }
    }
}
