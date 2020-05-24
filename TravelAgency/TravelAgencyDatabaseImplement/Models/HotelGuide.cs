﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyDatabaseImplement.Models
{
    public class HotelGuide
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int GuideId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int IsReserved { get; set; }
        public Hotel Hotel { get; set; }
        public Guide Guide { get; set; }
    }
}