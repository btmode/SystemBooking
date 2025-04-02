﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SystemBroni.Models;

namespace SystemBroni.Service
{
    public interface IVipRoomService
    {
        public VipRoom CreateVipRoom(VipRoom vipRoom);
        public IEnumerable<VipRoom> GetVipRooms(int pageNumber, int pageSize);
        public IEnumerable<VipRoom> GetVipRoomsByNumber(string name, int pageNumber, int pageSize);
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


        public VipRoom CreateVipRoom(VipRoom vipRoom)
        {
            _context.VipRooms.Add(vipRoom);
            _context.SaveChanges();
            return vipRoom;
        }

        // Получаем все комнаты с пагинацией
        public IEnumerable<VipRoom> GetVipRooms(int pageNumber, int pageSize)
        {
            return _context.VipRooms
                .OrderBy(v => v.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        // Поиск комнаты по номеру с учетом пагинации
        public IEnumerable<VipRoom> GetVipRoomsByNumber(string name, int pageNumber, int pageSize)
        {
            return _context.VipRooms
                .Where(v => v.Name == name)
                .OrderBy(v => v.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

       
        public VipRoom GetVipRoomById(Guid id)
        {
            return _context.VipRooms.Find(id);
        }

        
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

        
        public bool DeleteVipRoomById(Guid id)
        {
            var vipRoom = _context.VipRooms.Find(id);
            if (vipRoom == null)
                return false;

            _context.VipRooms.Remove(vipRoom);
            _context.SaveChanges();
            return true;
        }
    }
}
