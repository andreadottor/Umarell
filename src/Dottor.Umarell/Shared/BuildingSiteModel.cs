namespace Dottor.Umarell.Shared;

using System;

public  class BuildingSiteModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public string? FileName { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
