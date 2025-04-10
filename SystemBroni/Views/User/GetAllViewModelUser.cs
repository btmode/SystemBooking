using SystemBroni.Models;


namespace SystemBroni.Models;

public class GetAllViewModelUser
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<User> Users { get; set; } = [];
}