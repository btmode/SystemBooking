using SystemBroni.Models;

namespace SystemBroni.Views;

public class GetAllViewModelVipRoomBooking
{
    public string Term { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<VipRoomBooking> Bookings { get; set; } = [];
}