using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Admin.Villa.RealEstate.Models
{
    public class JobPosition
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } // e.g., "Software Developer", "UI Designer"

        [MaxLength(300)]
        public string Description { get; set; }
    }

    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("JobPosition")]
        public int JobPositionId { get; set; }

        public JobPosition JobPosition { get; set; } // navigation property

        [MaxLength(500)]
        public string Bio { get; set; } // detailed description

        [MaxLength(255)]
        public string ImagePath { get; set; } // e.g., "/images/employees/john_doe.png"
    }
}
