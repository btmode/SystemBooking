using SystemBroni.Models;

namespace SystemBroni.Views;

public class GetAllViewModelTableBooking
{
    public string Term { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<TableBooking> Bookings { get; set; } = [];
}