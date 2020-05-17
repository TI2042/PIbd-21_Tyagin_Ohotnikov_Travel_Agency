using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        [Required]
        public string HotelName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("TourId")]
        public virtual List<Tour> Tours { get; set; }
        [ForeignKey("GuideId")]
        public virtual List<HotelGuide> HotelGuides { get; set; }
    }
}