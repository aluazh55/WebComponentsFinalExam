using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Villa.RealEstate.Model
{
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
        public byte[]? Image { get; set; } = null;
    }
}
