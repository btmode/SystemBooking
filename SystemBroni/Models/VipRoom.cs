using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemBroni.Models
{
    public enum VipRoomStatus
    {
        Available,   // Свободен
        Booked,      // Забронирован
        Occupied     // Занят (клиенты пришли)

    }
    public class VipRoom
    {
        public Guid Id { get; set; }

        public string Name { get; set; } 

        public int Capacity { get; set; } 

        public VipRoomStatus Status { get; set; } = VipRoomStatus.Available; 
    }
}
