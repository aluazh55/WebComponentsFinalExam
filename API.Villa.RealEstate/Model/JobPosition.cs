using System.ComponentModel.DataAnnotations;

namespace API.Villa.RealEstate.Model
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
}
