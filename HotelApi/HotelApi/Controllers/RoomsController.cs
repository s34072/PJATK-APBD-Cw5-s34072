using Microsoft.AspNetCore.Mvc;
using HotelApi.Models;
using HotelApi.Services;

namespace HotelApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public IActionResult GetRooms([FromQuery] DateTime? date)
    {
        var rooms = _roomService.GetAllRooms(date);
        return Ok(rooms); 
    }

    [HttpPost]
    public IActionResult AddRoom([FromBody] Room room)
    {
        var createdRoom = _roomService.AddRoom(room, out string? error);
        
        if (error != null)
        {
            return BadRequest(new { Message = error }); 
        }

        return Created($"/api/rooms/{createdRoom!.Id}", createdRoom); 
    }
}