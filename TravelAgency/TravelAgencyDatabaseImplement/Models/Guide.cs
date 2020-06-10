using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Guide
    {
        public int Id { get; set; }
        [DisplayName("Тема гида")]
        [Required]
        public string GuideThemeName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("GuideId")]
        public virtual List<HotelGuide> HotelGuides { get; set; }
        [ForeignKey("GuideId")]
        public virtual List<RequestGuide> RequestGuides { get; set; }
    }
}
