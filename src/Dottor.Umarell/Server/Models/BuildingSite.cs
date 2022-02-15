namespace Dottor.Umarell.Server.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BuildingSite
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? FileContentType { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

    }
}
