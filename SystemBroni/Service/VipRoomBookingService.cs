
using Microsoft.EntityFrameworkCore;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface IVipRoomBookingService
    {
        public List<VipRoomBooking> GetAllBooking();
        public VipRoomBooking? GetByIdBooking(Guid id);
        public VipRoomBooking CreateBooking(VipRoomBooking booking);
        public Task UpdateBooking(Guid id, VipRoomBooking updateBooking);
        public Task DeleteBooking(Guid id);
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

        public List<VipRoomBooking> GetAllBooking()
        {
            return _context.VipRoomBookings
                .Include(b => b.User)
                .Include(b => b.VipRoom)
                .ToList();
        }

        public VipRoomBooking? GetByIdBooking(Guid id)
        {
            return _context.VipRoomBookings
                .Include(b => b.User)
                .Include(b => b.VipRoom)
                .FirstOrDefault(b => b.Id == id);
        }

        public async Task UpdateBooking(Guid id, VipRoomBooking updateBooking)
        {
            var booking = _context.VipRoomBookings
                .Include(b => b.User)
                .Include(b => b.VipRoom)
                .FirstOrDefault(b => b.Id == id);

            if (booking == null)
                throw new Exception("");


            var user = _context.Users.Find(updateBooking.User.Id);
            if (user != null)
            {
                user.Name = updateBooking.User.Name;
                user.Phone = updateBooking.User.Phone;
            }

            //booking.BookingTime = updateBooking.BookingTime;

         
            if (booking.VipRoom.Id != updateBooking.VipRoom.Id)
            {
                var newVipRoom = _context.VipRooms.Find(updateBooking.VipRoom.Id);
                if (newVipRoom != null)
                {
                    booking.VipRoom = newVipRoom;
                }
            }

            await _context.SaveChangesAsync();
           
        }

        public async Task DeleteBooking(Guid id)
        {
            var booking = _context.VipRoomBookings.Find(id);
            if (booking == null)
                throw new Exception("");

            _context.VipRoomBookings.Remove(booking);

            await _context.SaveChangesAsync();
        }
    }
}



