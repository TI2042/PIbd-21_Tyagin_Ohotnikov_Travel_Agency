using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Guide
    {
        public int Id { get; set; }
        [Required]
        public string GuideName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("GuideId")]
        public virtual List<HotelGuide> HotelGuides { get; set; }
        [ForeignKey("GuideId")]
        public virtual List<RequestGuide> RequestGuides { get; set; }
    }
}
