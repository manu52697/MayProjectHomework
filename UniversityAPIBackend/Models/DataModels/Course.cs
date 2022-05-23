using System.ComponentModel.DataAnnotations;

namespace UniversityAPIBackend.Models.DataModels
{

    public enum Level
    {
        Basic,
        Medium,
        Advanced,
        Expert
    }


    public class Course : BaseEntity
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public string TargetAudience { get; set; } = string.Empty;
        public string Goals { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public Level Level { get; set; }
        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();
         [Required]
         public Chapter Index { get; set; } = new Chapter();


    }

}


