//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SystemBroni.Models;

//namespace SystemBroni.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class BookingController : ControllerBase
//{
//    private readonly ApplicationDbContext _context;

//    public BookingController(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    // Метод для бронирования
//    [HttpPost]
//    public async Task<IActionResult> BookTable(int userId, int? tableId, int? vipRoomId)
//    {
//        if (tableId == null && vipRoomId == null)
//            return BadRequest("Необходимо выбрать столик или VIP-комнату.");

//        var booking = new Booking
//        {
//            UserId = userId,
//            TableId = tableId,
//            VipRoomId = vipRoomId,
//            BookingTime = DateTime.Now
//        };

//        _context.Bookings.Add(booking);

//        if (tableId.HasValue)
//        {
//            var table = await _context.Tables.FindAsync(tableId.Value);
//            if (table != null)
//            {
//                table.IsAvailable = false;
//                _context.Tables.Update(table);
//            }
//        }

//        if (vipRoomId.HasValue)
//        {
//            var vipRoom = await _context.VipRooms.FindAsync(vipRoomId.Value);
//            if (vipRoom != null)
//            {
//                vipRoom.IsAvailable = false;
//                _context.VipRooms.Update(vipRoom);
//            }
//        }

//        await _context.SaveChangesAsync();
//        return RedirectToAction("Index", "Home");
//    }

//    // Метод для отмены бронирования
//    [HttpPost]
//    public async Task<IActionResult> CancelBooking(int bookingId)
//    {
//        var booking = await _context.Bookings
//            .Include(b => b.Table)
//            .Include(b => b.VipRoom)
//            .FirstOrDefaultAsync(b => b.Id == bookingId);

//        if (booking == null)
//            return NotFound();

//        if (booking.Table != null)
//        {
//            booking.Table.IsAvailable = true;
//            _context.Tables.Update(booking.Table);
//        }

//        if (booking.VipRoom != null)
//        {
//            booking.VipRoom.IsAvailable = true;
//            _context.VipRooms.Update(booking.VipRoom);
//        }

//        _context.Bookings.Remove(booking);
//        await _context.SaveChangesAsync();

//        return RedirectToAction("Index", "Home");
//    }
//}
