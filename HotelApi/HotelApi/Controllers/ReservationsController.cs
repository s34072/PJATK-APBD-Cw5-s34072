using Microsoft.AspNetCore.Mvc;
using HotelApi.Models;
using HotelApi.Services;

namespace HotelApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;
    private readonly IRoomService _roomService;

    public ReservationsController(IReservationService reservationService, IRoomService roomService)
    {
        _reservationService = reservationService;
        _roomService = roomService;
    }

    [HttpPost]
    public IActionResult CreateReservation([FromBody] Reservation request)
    {
        var rooms = _roomService.GetAllRooms(null);
        var room = rooms.FirstOrDefault(r => r.Id == request.RoomId);
        
        if (room == null) return NotFound(new { Message = "Pokój nie istnieje." });

        var createdReservation = _reservationService.CreateReservation(request, room.Price, out string? error);

        if (error != null)
        {
            return BadRequest(new { Message = error }); 
        }

        return Created($"/api/reservations/{createdReservation!.Id}", createdReservation); 
    }

    [HttpDelete("{id}")]
    public IActionResult CancelReservation(int id)
    {
        var success = _reservationService.CancelReservation(id);
        
        if (!success)
        {
            return NotFound(new { Message = "Rezerwacja nie istnieje lub została już anulowana." });
        }

        return NoContent(); 
    }
}