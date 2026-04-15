using HotelApi.Models;

namespace HotelApi.Services;

public interface IReservationService
{
    Reservation? CreateReservation(Reservation reservation, decimal roomPrice, out string? errorMessage);
    bool CancelReservation(int id);
    bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut);
}