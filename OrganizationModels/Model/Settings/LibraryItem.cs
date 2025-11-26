using System.ComponentModel.DataAnnotations;

namespace OrganizationModels.Model.Settings
{
    public class LibraryItem
    {
        [Key]
        public Guid LibraryId { get; set; }

        public string? Title { get; set; }

        public string? Author { get; set; }

        public string? Genre { get; set; }

        public string? Description { get; set; }

        public string? Year { get; set; }

        public string? CoverImage { get; set; }

        public DateTime? CreatedAt { get; set; } 

        public DateTime? UpdatedAt { get; set; }

    }
}
