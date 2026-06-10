using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickSlot.Data;
using QuickSlot.DTOs;
namespace QuickSlot.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VenuesController : ControllerBase
{
    private readonly AppDbContext _context;

    public VenuesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetVenues()
    {
        var venues = await _context.Venues.ToListAsync();

        return Ok(venues);
    }

    [HttpGet("{id}/slots")]
    public async Task<IActionResult> GetSlots(
    int id,
    DateTime date)
    {
        var bookings = await _context.Bookings
            .Where(x =>
                x.VenueId == id &&
                x.BookingDate.Date == date.Date)
            .ToListAsync();

        var slots = new List<SlotResponseDto>();

        for (int hour = 6; hour <= 22; hour++)
        {
            var time = new TimeSpan(hour, 0, 0);

            slots.Add(new SlotResponseDto
            {
                Time = time.ToString(@"hh\:mm"),
                IsBooked = bookings.Any(x =>
                    x.SlotTime == time)
            });
        }

        return Ok(slots);
    }
}

