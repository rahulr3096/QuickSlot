namespace QuickSlot.DTOs;

public class CreateBookingDto
{
    public int UserId { get; set; }

    public int VenueId { get; set; }

    public DateTime BookingDate { get; set; }

    public TimeSpan SlotTime { get; set; }
}