

namespace SystemBroni.Models;

public class GetAllViewModelTable
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<Table> Tables { get; set; } = [];
}