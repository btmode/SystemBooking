using Microsoft.EntityFrameworkCore;
using SystemBroni.Models;

namespace SystemBroni.Service;

public class BookingNumberGenerator(ApplicationDbContext context)
{
    private Random _random = new Random();

    public async Task<int> GenerateUniqueNumberForTableAsync()
    {
        int number;
        bool exists;

        do
        {
            number = _random.Next(1000, 10000);
            exists = await context.TableBookings.AnyAsync(b => b.BookingNumber == number);
        } while (exists);

        return number;
    }

    public async Task<int> GenerateUniqueNumberForVipRoomAsync()
    {
        int number;
        bool exists;

        do
        {
            number = _random.Next(1000, 10000);
            exists = await context.VipRoomBookings.AnyAsync(b => b.BookingNumber == number);
        } while (exists);

        return number;
    }
}
