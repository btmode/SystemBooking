using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemBroni.Models
{
    public class Table
    {
        public Guid Id { get; set; }

        public string Name { get; set; } 

        public int Capacity { get; set; } 
 
    }
}
