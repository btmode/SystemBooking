using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using SystemBroni.Models;


namespace SystemBroni.Service
{
    public interface ITableService
    {
        public Task<Table> Create(Table table);
        public Task<List<Table>>  GetAllTableOrByName(string term, int pageNumber, int pageSize);
        public Task<Table?> GetById(Guid id);
        public Task Update(Guid id, Table updateTable);
        public Task Delete(Guid id);
    }

    public class TableService : ITableService
    {
        private readonly ApplicationDbContext _context;

        public TableService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Table> Create(Table table)
        {
            await _context.Tables.AddAsync(table);
            
            await _context.SaveChangesAsync();
            return table;
        }
      
        
        public async Task<List<Table>> GetAllTableOrByName(string term, int pageNumber, int pageSize)
        {

            return await _context.Tables
               .Where(t => t.Name.Contains(term))
               .OrderBy(t => t.Id)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
        }

       
        public async Task<Table?> GetById(Guid id)
        {
            return await _context.Tables.FindAsync(id);
        }

        
        public async Task Update(Guid id, Table updateTable)
        {
            await _context.Tables
                .Where(a => a.Id == id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(a => a.Name, updateTable.Name)
                    .SetProperty(a => a.Capacity, updateTable.Capacity));
           
        }

        
        public async Task Delete(Guid id)
        {
            await _context.Users
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
