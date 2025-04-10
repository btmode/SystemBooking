using SystemBroni.Models;


namespace SystemBroni.Views;

public class GetAllViewModelUser
{
    public string Term { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<User> Users { get; set; } = [];
}