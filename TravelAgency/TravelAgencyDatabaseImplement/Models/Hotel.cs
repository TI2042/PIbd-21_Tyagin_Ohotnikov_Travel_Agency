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
        public int Capacity { get; set; }
        public string Country { get; set; }
        [ForeignKey("HotelId")]
        public virtual List<HotelGuide> HotelGuides { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier SupplierID { get; set; }
    }
}