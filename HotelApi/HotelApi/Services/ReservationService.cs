using HotelApi.Models;

namespace HotelApi.Services;

public class ReservationService : IReservationService
{
    private readonly List<Reservation> _reservations = new();
    private int _nextId = 1;

    public bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
    {
        return !_reservations.Any(r => 
            r.RoomId == roomId && 
            !r.IsCancelled &&
            r.CheckIn < checkOut && r.CheckOut > checkIn);
    }

    public Reservation? CreateReservation(Reservation reservation, decimal roomPrice, out string? errorMessage)
    {
        errorMessage = null;

        if (reservation.CheckIn <= DateTime.Now.Date) { errorMessage = "Data zameldowania musi być w przyszłości."; return null; }
        if (reservation.CheckOut <= reservation.CheckIn) { errorMessage = "Data wymeldowania musi być późniejsza niż zameldowania."; return null; }
        
        if (!IsRoomAvailable(reservation.RoomId, reservation.CheckIn, reservation.CheckOut))
        {
            errorMessage = "Pokój jest niedostępny w wybranym terminie.";
            return null;
        }

        int days = (reservation.CheckOut.Date - reservation.CheckIn.Date).Days;
        reservation.TotalPrice = days * roomPrice;
        
        reservation.Id = _nextId++;
        reservation.Status = "Confirmed";
        reservation.IsCancelled = false;
        
        _reservations.Add(reservation);
        return reservation;
    }

    public bool CancelReservation(int id)
    {
        var reservation = _reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null || reservation.IsCancelled) return false;

        reservation.IsCancelled = true;
        reservation.Status = "Cancelled";
        return true;
    }
}