using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SystemBroni.Models;


namespace SystemBroni.Service
{
    public interface ITableService
    {
        public Table CreateTable(Table table);
        public IEnumerable<Table> GetTables(int pageNumber, int pageSize);
        public IEnumerable<Table> GetTablesByNumber(int number, int pageNumber, int pageSize);
        public Table GetTableById(Guid id);
        public bool UpdateTable(Guid id, Table updateTable);
        public bool DeleteTableById(Guid id);
    }

    public class TableService : ITableService
    {
        private readonly ApplicationDbContext _context;

        public TableService(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public Table CreateTable(Table table)
        {
            _context.Tables.Add(table);

            _context.SaveChanges();
            return table;
        }

       
        public IEnumerable<Table> GetTables(int pageNumber, int pageSize)
        {
            return _context.Tables.OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<Table> GetTablesByNumber(int number, int pageNumber, int pageSize)
        {
            return _context.Tables
                .Where(t => t.Number == number)
                .OrderBy(t => t.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        public Table GetTableById(Guid id)
        {
            return _context.Tables.Find(id);
        }

        public bool UpdateTable(Guid id, Table updateTable)
        {
            var table = _context.Tables.Find(id);

            if (table == null)
                return false;

            table.Number = updateTable.Number;
            table.Capacity = updateTable.Capacity;
            table.Status = updateTable.Status;

            _context.SaveChanges();
            return true;
        }

        public bool DeleteTableById(Guid id)
        {
            var table = _context.Tables.Find(id);

            if (table == null)
                return false;

            _context.Tables.Remove(table);

            _context.SaveChanges();
            return true;
        }
    }
}
