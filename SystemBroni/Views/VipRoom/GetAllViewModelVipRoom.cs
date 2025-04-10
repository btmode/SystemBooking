using SystemBroni.Models;

namespace SystemBroni.Views;

public class GetAllViewModelVipRoom
{
    public string Term { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<VipRoom> VipRooms { get; set; } = [];
}