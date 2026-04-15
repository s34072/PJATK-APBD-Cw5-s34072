namespace HotelApi.Models;

public class Reservation
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string GuestFullName { get; set; } = string.Empty;
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public bool IsCancelled { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = "New"; 
}