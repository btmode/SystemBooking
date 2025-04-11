using Microsoft.EntityFrameworkCore;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface ITableBookingService
    {
        public Task<TableBooking> Create(TableBooking booking, Table table, Guid? userId);
        public List<TableBooking> GetBookingsByUserName(string term, int pageNumber, int pageSize);
        public TableBooking? GetById(Guid id);
        public List<Table>? GetAllTables();
        public List<User>? GetAllUsers();
        public Task Update(TableBooking updateBooking);
        public Task Delete(Guid id);
    }

    public class TableBookingService : ITableBookingService
    {
        private readonly ApplicationDbContext _context;

        public TableBookingService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<bool> IsTableAvailableAsync(Guid tableId, DateTime startTime, DateTime endTime)
        {
            if (tableId == Guid.Empty)
                throw new ArgumentException("Не указан идентификатор стола.");

            if (startTime >= endTime)
                throw new ArgumentException("Начальное время должно быть раньше конечного времени.");

            return !await _context.TableBookings
                .AnyAsync(b => b.Table.Id == tableId &&
                               b.StartTime < endTime &&
                               b.EndTime > startTime);
        }


        public async Task<TableBooking> Create(TableBooking booking, Table table, Guid? userId)
        {
            if (userId == null || userId == Guid.Empty)
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == booking.User.Email &&
                                              u.Phone == booking.User.Phone);

                if (existingUser != null)
                {
                    userId = existingUser.Id;
                }
                else
                {
                    if (string.IsNullOrEmpty(booking.User?.Name) ||
                        string.IsNullOrEmpty(booking.User?.Email) ||
                        string.IsNullOrEmpty(booking.User?.Phone))
                    {
                        throw new ArgumentException(
                            "Поля пользователя (имя, email, телефон) обязательны к заполнению.");
                    }

                    var newUser = new User
                    {
                        Name = booking.User.Name,
                        Email = booking.User.Email,
                        Phone = booking.User.Phone
                    };

                    await _context.Users.AddAsync(newUser);
                    await _context.SaveChangesAsync();
                    userId = newUser.Id;
                }
            }

            if (userId != null && userId != Guid.Empty)
            {
                var existingUser = _context.Users
                    .FirstOrDefault(u => u.Id == userId);

                if (existingUser == null)
                    throw new InvalidOperationException("Пользователь не найден");

                booking.User = existingUser;
            }

            if (table?.Id == null || table.Id == Guid.Empty)
                throw new ArgumentException("Не указан идентификатор стола или объект стола пустой.");

            var existingTable = await _context.Tables
                .FirstOrDefaultAsync(t => t.Id == table.Id);

            booking.StartTime = booking.StartTime.ToUniversalTime();
            booking.EndTime = booking.EndTime.ToUniversalTime();

            if (existingTable == null)
                throw new InvalidOperationException("Столик не найден");

            if (!await IsTableAvailableAsync(existingTable.Id, booking.StartTime, booking.EndTime))
                throw new InvalidOperationException("Столик уже забронирован на выбранное время");

            booking.Table = existingTable;

            await _context.TableBookings.AddAsync(booking);

            await _context.SaveChangesAsync();
            return booking;
        }


        public List<User>? GetAllUsers()
        {
            return _context.Users.ToList();
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


        public List<TableBooking> GetBookingsByUserName(string term, int pageNumber, int pageSize)
        {
            return _context.TableBookings
                .Include(tb => tb.User)
                .Include(tb => tb.Table)
                .Where(n => n.User.Name.Contains(term))
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

        public async Task Update(TableBooking updateBooking)
        {
            await _context.TableBookings
                .Include(b => b.User)
                .Include(b => b.Table)
                .Where(b => b.Id == updateBooking.Id)
                .ExecuteUpdateAsync(a => a
                    .SetProperty(b => b.StartTime, updateBooking.StartTime)
                    .SetProperty(b => b.EndTime, updateBooking.EndTime)
                    .SetProperty(b => b.User, updateBooking.User)
                    .SetProperty(b => b.Table, updateBooking.Table));
        }


        public async Task Delete(Guid id)
        {
            await _context.TableBookings
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}