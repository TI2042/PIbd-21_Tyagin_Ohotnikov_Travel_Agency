using System.ComponentModel.DataAnnotations;

namespace TravelAgencyDatabaseImplement.Models
{
    public class RequestGuide
    {
        public int Id { get; set; }
        public int RequestID { get; set; }
        public int GuideId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public bool InHotel { get; set; }
        public virtual Guide Guide { get; set; }
        public virtual Request Request { get; set; }
    }
}
