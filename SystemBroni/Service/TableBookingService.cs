using Microsoft.EntityFrameworkCore;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface ITableBookingService
    {
        public IEnumerable<TableBooking> GetAllBooking();
        public TableBooking GetByIdBooking(Guid id);
        public TableBooking CreateBooking(TableBooking booking);
        public bool UpdateBooking(Guid id, TableBooking updateBooking);
        public bool DeleteBooking(Guid id);
    }
    public class TableBookingService : ITableBookingService
    {
        private readonly ApplicationDbContext _context;

        public TableBookingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public TableBooking CreateBooking(TableBooking booking)
        {
            _context.TableBookings.Add(booking);
            _context.SaveChanges();
            return booking;
        }
        public IEnumerable<TableBooking> GetAllBooking()
        {
            return _context.TableBookings
                .Include(b => b.User)
                .Include(b => b.Table)
                .ToList();
        }

        public TableBooking GetByIdBooking(Guid id)
        {
            return _context.TableBookings
                .Include(b => b.User)
                .Include(b => b.Table)
                .FirstOrDefault(b => b.Id == id);
        }

        public bool UpdateBooking(Guid id, TableBooking updateBooking)
        {
            var booking = _context.TableBookings
                .Include(b => b.User)
                .Include(b => b.Table)
                .FirstOrDefault(b => b.Id == id);

            if (booking == null)
            {
                return false;
            }

            var user = _context.Users.Find(updateBooking.User.Id);
            if (user != null)
            {
                user.Name = updateBooking.User.Name;
                user.Phone = updateBooking.User.Phone;
            }

            booking.BookingTime = updateBooking.BookingTime;

            if (booking.Table.Id != updateBooking.Table.Id)
            {
                var newTable = _context.Tables.Find(updateBooking.Table.Id);
                if (newTable != null)
                {
                    booking.Table = newTable;
                }
            }

            _context.SaveChanges();
            return true;

        }

        public bool DeleteBooking(Guid id)
        {
            var booking = _context.TableBookings.Find(id);
            if (booking == null)
            { 
                return false;
            }

            _context.TableBookings.Remove(booking);
            _context.SaveChanges();
            return true;
        }

    }
}
