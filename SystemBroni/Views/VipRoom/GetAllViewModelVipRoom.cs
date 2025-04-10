

namespace SystemBroni.Models;

public class GetAllViewModelVipRoom
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<VipRoom> Bookings { get; set; } = [];
}