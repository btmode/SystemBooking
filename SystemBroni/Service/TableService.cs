using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using SystemBroni.Models;


namespace SystemBroni.Service
{
    public interface ITableService
    {
        public Task<Table> CreateTable(Table table);
        public List<Table> GetTables(int pageNumber, int pageSize);
        public List<Table> GetTablesByName(string name, int pageNumber, int pageSize);
        public Table? GetTableById(Guid id);
        public Task UpdateTable(Guid id, Table updateTable);
        public Task DeleteTableById(Guid id);
    }

    public class TableService : ITableService
    {
        private readonly ApplicationDbContext _context;

        public TableService(ApplicationDbContext context)
        {
            _context = context;
        }


        // это я переделал под Async
        public async Task<Table> CreateTable(Table table)
        {
            await _context.Tables.AddAsync(table);

            await _context.SaveChangesAsync();
            return table;
        }

        // здесь не нужен Async
        public List<Table> GetTables(int pageNumber, int pageSize)
        {
            return _context.Tables.OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        // здесь не нужен Async
        public List<Table> GetTablesByName(string name, int pageNumber, int pageSize)
        {

            return _context.Tables
               .Where(t => t.Name.Contains(name))
               .OrderBy(t => t.Id)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToList();
        }

        // здесь не нужен Async
        public Table? GetTableById(Guid id)
        {
            return _context.Tables.Find(id);
        }

        // здесь нужен Async только для SaveChangesAsync
        public async Task UpdateTable(Guid id, Table updateTable)
        {
            var table = _context.Tables.Find(id);

            if (table == null)
                throw new Exception("");

            table.Name = updateTable.Name;
            table.Capacity = updateTable.Capacity;

            await _context.SaveChangesAsync();
        }

        // здесь нужен Async только для SaveChangesAsync
        public async Task DeleteTableById(Guid id)
        {
            var table = _context.Tables.Find(id);

            if (table == null)
                throw new Exception("");

            _context.Tables.Remove(table);

            await _context.SaveChangesAsync();

        }
    }
}
