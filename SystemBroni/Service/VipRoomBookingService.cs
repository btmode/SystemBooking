
using Microsoft.EntityFrameworkCore;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface IVipRoomBookingService
    {
        public Task<VipRoomBooking> Create(VipRoomBooking booking, Guid? userId);
        public Task<List<VipRoomBooking>> GetAllBookingsOrByUserName(string term, int pageNumber, int pageSize);
        public Task<VipRoomBooking?> GetById(Guid id);
        public List<VipRoom>? GetAllVipRooms();
        public List<User>? GetAllUsers();
        public Task Update(VipRoomBooking updateBooking);
        public Task Delete(Guid id);
        
    }

    public class VipRoomBookingService : IVipRoomBookingService
    {
        private readonly ApplicationDbContext _context;

        public VipRoomBookingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsTableAvailableAsync(Guid tableId, DateTime startTime, DateTime endTime)
        {
            if (startTime >= endTime)
                throw new ArgumentException("Начальное время должно быть раньше конечного времени.");

            return !await _context.VipRoomBookings
                .AnyAsync(b =>
                    b.VipRoom.Id == tableId &&
                    b.StartTime < endTime &&
                    b.EndTime > startTime);
        }
        
        public async Task<VipRoomBooking> Create(VipRoomBooking booking, Guid? userId)
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


                if (booking.VipRoom?.Id == null || booking.VipRoom.Id == Guid.Empty)
                    throw new ArgumentException("Не указан идентификатор Vip-комнаты или объект Vip-комнаты пустой.");

                var existingTable = await _context.VipRooms
                    .FirstOrDefaultAsync(t => t.Id == booking.VipRoom.Id);

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

                booking.VipRoom = existingTable;

                await _context.VipRoomBookings.AddAsync(booking);

                await _context.SaveChangesAsync();
                
                await transaction.CommitAsync();
                return booking;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public List<User>? GetAllUsers()
        {
            return _context.Users.ToList();
        }
        
        public List<VipRoom>? GetAllVipRooms()
        {
            return _context.VipRooms.ToList();
        }
        
        public async Task<List<VipRoomBooking>> GetAllBookingsOrByUserName(string term, int pageNumber, int pageSize)
        {
            return await _context.VipRoomBookings
                .Include(tb => tb.User)
                .Include(tb => tb.VipRoom)
                .Where(n => n.User.Name.Contains(term))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        
        public async Task<VipRoomBooking?> GetById(Guid id)
        {
            return await _context.VipRoomBookings
                .Include(tb => tb.User)
                .Include(tb => tb.VipRoom)
                .FirstOrDefaultAsync(tb => tb.Id == id);
        }
        
        public async Task Update(VipRoomBooking updateBooking)
        {
            await _context.VipRoomBookings
                .Include(b => b.User)
                .Include(b => b.VipRoom)
                .Where(b => b.Id == updateBooking.Id)
                .ExecuteUpdateAsync(a => a
                    .SetProperty(b => b.StartTime, updateBooking.StartTime)
                    .SetProperty(b => b.EndTime, updateBooking.EndTime)
                    .SetProperty(b => b.User, updateBooking.User)
                    .SetProperty(b => b.VipRoom, updateBooking.VipRoom));
        }
        
        public async Task Delete(Guid id)
        {
            await _context.VipRoomBookings
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync();
        }
        
    }
}



