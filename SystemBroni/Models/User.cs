using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemBroni.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }

        // Связь с бронированиями
        public List<TableBooking> TableBookings { get; set; } = new();
        public List<VipRoomBooking> VipRoomBookings { get; set; } = new();
    }
}
