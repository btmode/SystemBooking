

namespace SystemBroni.Models;

// Какие поля нужно отправить в Html
public class GetAllViewModel
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<TableBooking> Bookings { get; set; } = [];
}