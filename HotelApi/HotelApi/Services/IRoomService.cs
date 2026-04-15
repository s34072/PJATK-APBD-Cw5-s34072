using HotelApi.Models;

namespace HotelApi.Services;

public interface IRoomService
{
    List<Room> GetAllRooms(DateTime? date);
    Room? AddRoom(Room room, out string? errorMessage);
}