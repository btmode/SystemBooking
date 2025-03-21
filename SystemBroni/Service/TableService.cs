using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface ITableService
    {
        public Table CreateTable(Table table);
        public IEnumerable<Table> GetTables();
        public Table GetTableById(int id);
        public bool UpdateTable(int id, Table updateTable);
        public bool DeleteTableById(int id);
    }
  
    public class TableService : ITableService
    {
        private readonly ApplicationDbContext _context;
        private readonly List<int> _deletedIds = []; // храним удаленные ID tables

        public TableService(ApplicationDbContext context)
        {
            _context = context;
        }        

        // создать новый стол
        public Table CreateTable(Table table)
        {
            _context.Tables.Add(table);

            _context.SaveChanges();
            return table;
        }

        // получаем все столы сразу
        public IEnumerable<Table> GetTables()
        {
            return _context.Tables.OrderBy(u => u.Id).ToList();
        }

        public Table GetTableById(int id)
        {
            return _context.Tables.Find(id);
        }

        public bool UpdateTable(int id, Table updateTable)
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

        public bool DeleteTableById(int id)
        {
            var table = _context.Tables.Find(id);            

            if(table == null)
            {
                return false;
            }
            _context.Tables.Remove(table);

            _context.SaveChanges();

            _deletedIds.Add(id);

            return true;
        }
    }
}
