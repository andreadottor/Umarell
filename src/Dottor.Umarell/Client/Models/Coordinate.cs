namespace Dottor.Umarell.Client.Models
{
    public record Coordinate
    {
        public static readonly Coordinate Empty = new Coordinate();

        public Coordinate()
        { }

        public Coordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
