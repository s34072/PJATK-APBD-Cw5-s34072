namespace HotelApi.Models;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MaxCapacity { get; set; }
    public decimal Price { get; set; }
    public bool IsSmoking { get; set; }
    public bool HasAirConditioning { get; set; }
}