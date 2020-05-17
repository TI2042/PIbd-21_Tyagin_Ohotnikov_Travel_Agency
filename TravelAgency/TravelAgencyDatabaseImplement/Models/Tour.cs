using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Tour
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime? CompletionDate { get; set; }
        [Required]
        public Status Status { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}