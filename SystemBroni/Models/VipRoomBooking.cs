using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SystemBroni.Models
{
    public class VipRoomBooking
    {
        public Guid Id { get; set; }
        public User? User { get; set; }
        public VipRoom? VipRoom { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int BookingNumber { get; set; }
    }
}
