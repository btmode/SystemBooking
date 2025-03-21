using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface IVipRoomService
    {
        public VipRoom CreateVipRoom(VipRoom VipRoom);
        public IEnumerable<VipRoom> GetVipRooms();
        public VipRoom GetVipRoomById(int id);
        public bool UpdateVipRoom(int id, VipRoom updateVipRoom);
        public bool DeleteVipRoomById(int id);
    }

    
    public class VipRoomService : IVipRoomService
    {
        private readonly ApplicationDbContext _context;
        private readonly List<int> _deletedIds = []; // храним удаленные ID tables

        public VipRoomService(ApplicationDbContext context)
        {
            _context = context;
        }
       
        // создать новую комнату
        public VipRoom CreateVipRoom(VipRoom vipRoom)
        {
            
            _context.VipRooms.Add(vipRoom);

            _context.SaveChanges();
            return vipRoom;
        }

        // получаем все столы сразу
        public IEnumerable<VipRoom> GetVipRooms()
        {
            return _context.VipRooms.OrderBy(u => u.Id).ToList();
        }

        // Получить VipRoom по ID
        public VipRoom GetVipRoomById(int id)
        {
            return _context.VipRooms.Find(id);
        }

        // Обновить данные VipRoom
        public bool UpdateVipRoom(int id, VipRoom updateVipRoom)
        {
            var vipRoom = _context.VipRooms.Find(id);

            if (vipRoom == null)
                return false;

            vipRoom.Name = updateVipRoom.Name;
            vipRoom.Capacity = updateVipRoom.Capacity;
            vipRoom.Status = updateVipRoom.Status;

            _context.SaveChanges();
            return true;
        }

        // Удалить VipRoom по ID
        public bool DeleteVipRoomById(int id)
        {
            var vipRoom = _context.VipRooms.Find(id);

            if (vipRoom == null)
            {
                return false;
            }
            _context.VipRooms.Remove(vipRoom);

            _context.SaveChanges(); 

            _deletedIds.Add(id);

            return true;
        }
    }
}
