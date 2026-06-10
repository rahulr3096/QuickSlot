using QuickSlot.Models;

public class Booking
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int VenueId { get; set; }

    public DateTime BookingDate { get; set; }

    public TimeSpan SlotTime { get; set; }

    public User? User { get; set; }

    public Venue? Venue { get; set; }
}