using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface IVipRoomService
    {
        public Task<VipRoom> CreateVipRoom(VipRoom vipRoom);
        public List<VipRoom> GetVipRooms(int pageNumber, int pageSize);
        public List<VipRoom> GetVipRoomsByNumber(string name, int pageNumber, int pageSize);
        public VipRoom? GetVipRoomById(Guid id);
        public Task UpdateVipRoom(Guid id, VipRoom updateVipRoom);
        public Task DeleteVipRoomById(Guid id);
    }


    public class VipRoomService : IVipRoomService
    {
        private readonly ApplicationDbContext _context;

        public VipRoomService(ApplicationDbContext context)
        {
            _context = context;
        }

        // здесь я переделал под Async
        public async Task<VipRoom> CreateVipRoom(VipRoom vipRoom)
        {
            await _context.VipRooms.AddAsync(vipRoom);

            await _context.SaveChangesAsync();
            return vipRoom;
        }


        // здесь Async не нужен
        public List<VipRoom> GetVipRooms(int pageNumber, int pageSize)
        {
            return _context.VipRooms
                .OrderBy(v => v.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        // здесь Async не нужен
        public List<VipRoom> GetVipRoomsByNumber(string name, int pageNumber, int pageSize)
        {
            return _context.VipRooms
                .Where(v => v.Name.Contains(name))
                .OrderBy(v => v.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        // здесь Async не нужен
        public VipRoom? GetVipRoomById(Guid id)
        {
            return _context.VipRooms.Find(id);
        }

        // здесь Async нужен только для SaveChangesAsync
        public async Task UpdateVipRoom(Guid id, VipRoom updateVipRoom)
        {
            var vipRoom = _context.VipRooms.Find(id);
            if (vipRoom == null)
                throw new Exception("");

            vipRoom.Name = updateVipRoom.Name;
            vipRoom.Capacity = updateVipRoom.Capacity;

            await _context.SaveChangesAsync();
        }

        // здесь Async нужен только для SaveChangesAsync
        public async Task DeleteVipRoomById(Guid id)
        {
            var vipRoom = _context.VipRooms.Find(id);
            if (vipRoom == null)
                throw new Exception("");

            _context.VipRooms.Remove(vipRoom);

            await _context.SaveChangesAsync();
        }
    }
}
