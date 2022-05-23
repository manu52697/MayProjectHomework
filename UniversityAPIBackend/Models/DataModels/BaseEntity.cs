using System.ComponentModel.DataAnnotations;

namespace UniversityAPIBackend.Models.DataModels
{
    public class BaseEntity
    {

        [Required]
        [Key]
        public int Id { get; set; }

        public  Guid CreatedBy { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid UpdatedBy { get; set; } = Guid.NewGuid();
        public DateTime? UpdatedAt { get; set; }
        public Guid DeletedBy { get; set; } = Guid.NewGuid();
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
