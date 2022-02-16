namespace Dottor.Umarell.Shared
{
    
    public class BuildingSiteInsertModel
    {

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public string? FileName { get; set; }
        public byte[]? FileContent { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
