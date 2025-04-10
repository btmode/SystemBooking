using SystemBroni.Models;

namespace SystemBroni.Views;

public class GetAllViewModelTable
{
    public string Term { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<Table> Tables { get; set; } = [];
}