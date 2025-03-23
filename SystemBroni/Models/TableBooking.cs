using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SystemBroni.Models
{
    public class TableBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public User User { get; set; }

        public Table Table { get; set; }

        [Required]
        public DateTime BookingTime { get; set; }
    }
}

