using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface IVipRoomService
    {
        public Task<VipRoom> Create(VipRoom vipRoom);
        public Task<List<VipRoom>>  GetVipRoomsOrByName(string term, int pageNumber, int pageSize);
        public Task<VipRoom?> GetVipRoomById(Guid id);
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


        public async Task<VipRoom> Create(VipRoom vipRoom)
        {
            await _context.VipRooms.AddAsync(vipRoom);

            await _context.SaveChangesAsync();
            return vipRoom;
        }




        public async Task<List<VipRoom>> GetVipRoomsOrByName(string term, int pageNumber, int pageSize)
        {
            return await _context.VipRooms
                .Where(v => v.Name.Contains(term))
                .OrderBy(v => v.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<VipRoom?> GetVipRoomById(Guid id)
        {
            return await _context.VipRooms.FindAsync(id);
        }


        public async Task UpdateVipRoom(Guid id, VipRoom updateVipRoom)
        {
            await _context.VipRooms
                .Where(a => a.Id == id)
                .ExecuteUpdateAsync(a => a
                    .SetProperty(n => n.Name, updateVipRoom.Name)
                    .SetProperty(n => n.Capacity, updateVipRoom.Capacity));
        }


        public async Task DeleteVipRoomById(Guid id)
        {
            await _context.VipRooms
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}