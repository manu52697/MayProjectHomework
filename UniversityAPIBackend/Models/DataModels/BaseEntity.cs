using System.ComponentModel.DataAnnotations;

namespace UniversityAPIBackend.Models.DataModels
{
    public class BaseEntity
    {

        [Required]
        [Key]
        public int Id { get; set; }

        public User? CreatedBy { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public User? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public User? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
