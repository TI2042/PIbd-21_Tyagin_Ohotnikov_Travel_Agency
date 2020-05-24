using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Tour
    {
        public int Id { get; set; }
        [Required]
        public string TourName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("TourId")]
        public virtual List<Order> Orders { get; set; }
        [ForeignKey("TourId")]
        public virtual List<TourGuide> TourGuides { get; set; }
    }
}