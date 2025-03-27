
using Microsoft.EntityFrameworkCore;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface IVipRoomBookingService
    {
        public IEnumerable<VipRoomBooking> GetAllBooking();
        public VipRoomBooking GetByIdBooking(Guid id);
        public VipRoomBooking CreateBooking(VipRoomBooking booking);
        public bool UpdateBooking(Guid id, VipRoomBooking updateBooking);
        public bool DeleteBooking(Guid id);
    }

    public class VipRoomBookingService : IVipRoomBookingService
    {
        private readonly ApplicationDbContext _context;

        public VipRoomBookingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public VipRoomBooking CreateBooking(VipRoomBooking booking)
        {
            _context.VipRoomBookings.Add(booking);
            _context.SaveChanges();
            return booking;
        }

        public IEnumerable<VipRoomBooking> GetAllBooking()
        {
            return _context.VipRoomBookings
                .Include(b => b.User)
                .Include(b => b.VipRoom)
                .ToList();
        }

        public VipRoomBooking GetByIdBooking(Guid id)
        {
            return _context.VipRoomBookings
                .Include(b => b.User)
                .Include(b => b.VipRoom)
                .FirstOrDefault(b => b.Id == id);
        }

        public bool UpdateBooking(Guid id, VipRoomBooking updateBooking)
        {
            var booking = _context.VipRoomBookings
                .Include(b => b.User)
                .Include(b => b.VipRoom)
                .FirstOrDefault(b => b.Id == id);

            if (booking == null)
            {
                return false;
            }

            // Обновляем данные пользователя
            var user = _context.Users.Find(updateBooking.User.Id);
            if (user != null)
            {
                user.Name = updateBooking.User.Name;
                user.Phone = updateBooking.User.Phone;
            }

            // Обновляем время бронирования
            booking.BookingTime = updateBooking.BookingTime;

            // Обновляем VIP-комнату, если она изменена
            if (booking.VipRoom.Id != updateBooking.VipRoom.Id)
            {
                var newVipRoom = _context.VipRooms.Find(updateBooking.VipRoom.Id);
                if (newVipRoom != null)
                {
                    booking.VipRoom = newVipRoom;
                }
            }

            _context.SaveChanges();
            return true;
        }

        public bool DeleteBooking(Guid id)
        {
            var booking = _context.VipRoomBookings.Find(id);
            if (booking == null) return false;

            _context.VipRoomBookings.Remove(booking);
            _context.SaveChanges();
            return true;
        }
    }
}



