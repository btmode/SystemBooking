using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using SystemBroni.Models;
using Table = SystemBroni.Models.Table;

namespace SystemBroni.Service
{
    public interface ITableBookingService
    {
        public Task<TableBooking> Create(TableBooking booking, Table table);
        public List<TableBooking> GetAll(int pageNumber, int pageSize);
        public List<TableBooking> GetBookingsByUserName(string name, int pageNumber, int pageSize);
        public TableBooking? GetById(Guid id);
        public List<Table>? GetAllTables();
        public Task UpdateBooking(TableBooking updateBooking);
        public Task Delete(Guid id);
    }

    public class TableBookingService : ITableBookingService
    {
        private readonly ApplicationDbContext _context;

        public TableBookingService(ApplicationDbContext context)
        {
            _context = context;
        }


        public bool IsTableAvailable(Guid tableId, DateTime startTime, DateTime endTime)
        {
            return !_context.TableBookings
                .Any(b => b.Table.Id == tableId &&
                          ((b.StartTime < endTime) && (b.EndTime > startTime)));
        }

        public async Task<TableBooking> Create(TableBooking booking, Table table)
        {
            var existingUser =  _context.Users
                        .FirstOrDefault(u => u.Email == booking.User.Email || u.Phone == booking.User.Phone);


            if (existingUser == null)
            {
                var newUser = new User
                {
                    Name = booking.User.Name,
                    Email = booking.User.Email,
                    Phone = booking.User.Phone
                };

                await _context.Users.AddAsync(newUser);
                booking.User = newUser;
            }
            else
            {
                booking.User = existingUser;
            }

            var existingTable =  _context.Tables.FirstOrDefault(t => t.Id == table.Id);

            if (existingTable == null)
                throw new Exception("Столик не найден");

            if (!IsTableAvailable(existingTable.Id, booking.StartTime, booking.EndTime))
                throw new Exception("Столик уже забронирован на выбранное время");

            booking.Table = existingTable;

            booking.StartTime = DateTime.SpecifyKind(booking.StartTime, DateTimeKind.Utc);
            booking.EndTime = DateTime.SpecifyKind(booking.EndTime, DateTimeKind.Utc);

            await _context.TableBookings.AddAsync(booking);

            await _context.SaveChangesAsync();
            return booking;
        }



        public List<TableBooking> GetAll(int pageNumber, int pageSize)
        {
            return _context.TableBookings
               .Include(tb => tb.User)
               .Include(tb => tb.Table)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToList();
        }

        public List<Table>? GetAllTables()
        {
            return _context.Tables.ToList();
        }


        public List<TableBooking> GetBookingsByUserName(string name, int pageNumber, int pageSize)
        {
            return _context.TableBookings
                .Include(tb => tb.User)
                .Include(tb => tb.Table)
                .Where(tb => tb.User.Name.Contains(name))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public TableBooking? GetById(Guid id)
        {
            return _context.TableBookings
                .Include(tb => tb.User)
                .Include(tb => tb.Table)
                .FirstOrDefault(tb => tb.Id == id);
        }

        public async Task UpdateBooking(TableBooking updateBooking)
        {
            var booking = _context.TableBookings
                .Include(b => b.User)
                .Include(b => b.Table)
                .FirstOrDefault(b => b.Id == updateBooking.Id);

            if (booking == null)
                throw new Exception("Бронирование не найдено!");


            if (booking.Table != null && updateBooking.Table != null && booking.Table.Id != updateBooking.Table.Id)
            {
                var newTable = _context.Tables.Find(updateBooking.Table.Id);
                if (newTable != null)
                {
                    booking.Table = newTable;
                }
            }

            await _context.SaveChangesAsync();
        }


        public async Task Delete(Guid id)
        {
            var booking = _context.TableBookings.Find(id);

            if (booking == null)
                throw new Exception("");

            _context.TableBookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
}
