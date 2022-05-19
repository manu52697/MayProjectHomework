using System.ComponentModel.DataAnnotations;

namespace UniversityAPIBackend.Models.DataModels
{

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
        public CourseLevels level { get; set; }


    }

}


