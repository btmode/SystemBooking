using Microsoft.EntityFrameworkCore;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface ITableBookingService
    {
        public Task<TableBooking> Create(TableBooking booking,Guid? userId);
        public Task<List<TableBooking>> GetAllBookingsOrByUserName(string term, int pageNumber, int pageSize);
        public Task<TableBooking?> GetById(Guid id);
        public Task<List<Table?>> GetAllTables();
        public Task Update(TableBooking updateBooking);
        public Task Delete(Guid id);
    }

    public class TableBookingService : ITableBookingService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TableBookingService> _logger;

        public TableBookingService(ApplicationDbContext context, ILogger<TableBookingService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<bool> IsTableAvailableAsync(Guid tableId, DateTime startTime, DateTime endTime)
        {
            if (startTime >= endTime)
                throw new ArgumentException("Начальное время должно быть раньше конечного времени.");

            return !await _context.TableBookings
                .AnyAsync(b =>
                    b.Table.Id == tableId &&
                    b.StartTime < endTime &&
                    b.EndTime > startTime);
        }


        public async Task<TableBooking> Create(TableBooking booking, Guid? userId)
        {
            await using var transaction = await _context.Database
                .BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

            try
            {
                if (userId is null || userId == Guid.Empty)
                {
                    var existingUser = await _context.Users
                        .FirstOrDefaultAsync(u => u.Email == booking.User.Email);


                    if (existingUser is not null)
                    {
                        userId = existingUser.Id;
                        booking.User = existingUser;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(booking.User?.Name) ||
                            string.IsNullOrEmpty(booking.User?.Email) ||
                            string.IsNullOrEmpty(booking.User?.Phone))
                            throw new ArgumentException(
                                "Поля пользователя (имя, email, телефон) обязательны к заполнению.");


                        var newUser = new User
                        {
                            Name = booking.User.Name,
                            Email = booking.User.Email,
                            Phone = booking.User.Phone
                        };

                        await _context.Users.AddAsync(newUser);
                        await _context.SaveChangesAsync();
                        userId = newUser.Id;
                        booking.User = newUser;
                    }
                }

                else if (userId is not null && userId != Guid.Empty)
                {
                    var existingUser = await _context.Users
                        .FirstOrDefaultAsync(u => u.Id == userId);

                    if (existingUser == null)
                        throw new InvalidOperationException("Пользователь не найден");

                    booking.User = existingUser;
                }


                if (booking.Table?.Id == null || booking.Table.Id == Guid.Empty)
                    throw new ArgumentException("Не указан идентификатор стола или объект стола пустой.");

                var existingTable = await _context.Tables
                    .FirstOrDefaultAsync(t => t.Id == booking.Table.Id);

                booking.StartTime = DateTime.SpecifyKind
                        (booking.StartTime, DateTimeKind.Local)
                    .ToUniversalTime();

                booking.EndTime = DateTime.SpecifyKind
                        (booking.EndTime, DateTimeKind.Local)
                    .ToUniversalTime();


                if (existingTable == null)
                    throw new InvalidOperationException("Столик не найден");

                if (!await IsTableAvailableAsync(existingTable.Id, booking.StartTime, booking.EndTime))
                    throw new InvalidOperationException("Столик уже забронирован на выбранное время");

                booking.Table = existingTable;

                await _context.TableBookings.AddAsync(booking);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return booking;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании бронирования");
                await transaction.RollbackAsync();
                throw;
            }
        }

        
        public async Task<List<Table?>> GetAllTables()
        {
                return await _context.Tables.ToListAsync();
        }


        public async Task<List<TableBooking>> GetAllBookingsOrByUserName(string term, int pageNumber, int pageSize)
        {
            return await _context.TableBookings
                .Include(tb => tb.User)
                .Include(tb => tb.Table)
                .Where(n => n.User.Name.Contains(term))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<TableBooking?> GetById(Guid id)
        {
            return await _context.TableBookings
                .Include(tb => tb.User)
                .Include(tb => tb.Table)
                .FirstOrDefaultAsync(tb => tb.Id == id);
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