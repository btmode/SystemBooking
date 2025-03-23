using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SystemBroni.Models
{
    public class TableBooking
    {
        public Guid Id { get; set; }

        public User User { get; set; }

        public Table Table { get; set; }

        public DateTime BookingTime { get; set; }
    }
}

