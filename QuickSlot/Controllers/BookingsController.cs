using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickSlot.Data;
using QuickSlot.DTOs;
using QuickSlot.Models;

namespace QuickSlot.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BookingsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking(
        CreateBookingDto dto)
    {
        try
        {
            var booking = new Booking
            {
                UserId = dto.UserId,
                VenueId = dto.VenueId,
                BookingDate = dto.BookingDate.Date,
                SlotTime = dto.SlotTime
            };

            _context.Bookings.Add(booking);

            await _context.SaveChangesAsync();

            return Ok(booking);
        }
        catch (DbUpdateException)
        {
            return Conflict(new
            {
                message = "Slot already booked"
            });
        }
    }



    [HttpGet("/api/users/{id}/bookings")]
    public async Task<IActionResult> GetUserBookings(
    int id)
    {
        var bookings = await _context.Bookings
            .Where(x => x.UserId == id)
            .ToListAsync();

        return Ok(bookings);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(
    int id)
    {
        var booking = await _context.Bookings
            .FindAsync(id);

        if (booking == null)
        {
            return NotFound();
        }

        _context.Bookings.Remove(booking);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}