using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyDatabaseImplement.Models
{
    public class TourGuide
    {
        public int Id { get; set; }
        [Required]
        public int Count { get; set; }
        public int GuideId { get; set; }
        public int TourId { get; set; }
        public Guide Guide { get; set; }
        public Tour Tour { get; set; }
    }
}
