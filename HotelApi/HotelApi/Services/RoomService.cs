using HotelApi.Models;

namespace HotelApi.Services;

public class RoomService : IRoomService
{
    private readonly IReservationService _reservationService;

    private readonly List<Room> _rooms = new()
    {
        new Room { Id = 1, Name = "Pokój Jednoosobowy", MaxCapacity = 1, Price = 150, IsSmoking = false, HasAirConditioning = true },
        new Room { Id = 2, Name = "Apartament Królewski", MaxCapacity = 4, Price = 500, IsSmoking = false, HasAirConditioning = true }
    };
    private int _nextId = 3;
    
    public RoomService(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public List<Room> GetAllRooms(DateTime? date)
    {
        // Jeśli nie podano daty (null), zwracamy wszystkie pokoje
        if (!date.HasValue)
        {
            return _rooms;
        }

        // Jeśli podano datę, używamy funkcji IsRoomAvailable z serwisu rezerwacji
        return _rooms.Where(room => 
            _reservationService.IsRoomAvailable(room.Id, date.Value.Date, date.Value.Date.AddDays(1))
        ).ToList();
    }

    public Room? AddRoom(Room room, out string? errorMessage)
    {
        errorMessage = null;

        if (room.Price <= 0) { errorMessage = "Cena musi być większa od zera."; return null; }
        if (room.MaxCapacity <= 0) { errorMessage = "Pojemność pokoju musi być większa od zera."; return null; }

        room.Id = _nextId++;
        _rooms.Add(room);
        return room;
    }
}