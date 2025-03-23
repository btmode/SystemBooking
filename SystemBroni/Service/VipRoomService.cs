using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface IVipRoomService
    {
        public VipRoom CreateVipRoom(VipRoom VipRoom);
        public IEnumerable<VipRoom> GetVipRooms();
        public VipRoom GetVipRoomById(Guid id);
        public bool UpdateVipRoom(Guid id, VipRoom updateVipRoom);
        public bool DeleteVipRoomById(Guid id);
    }

    
    public class VipRoomService : IVipRoomService
    {
        private readonly ApplicationDbContext _context;

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
        public VipRoom GetVipRoomById(Guid id)
        {
            return _context.VipRooms.Find(id);
        }

        // Обновить данные VipRoom
        public bool UpdateVipRoom(Guid id, VipRoom updateVipRoom)
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
        public bool DeleteVipRoomById(Guid id)
        {
            var vipRoom = _context.VipRooms.Find(id);

            if (vipRoom == null)
            {
                return false;
            }
            _context.VipRooms.Remove(vipRoom);

            _context.SaveChanges(); 
            return true;
        }
    }
}
