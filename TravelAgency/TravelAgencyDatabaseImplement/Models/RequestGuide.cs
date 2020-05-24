using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyDatabaseImplement.Models
{
    public class RequestGuide
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int GuideId { get; set; }
        [Required]
        public int Count { get; set; }
        public Guide Guide { get; set; }
        public Request Request { get; set; }
    }
}
