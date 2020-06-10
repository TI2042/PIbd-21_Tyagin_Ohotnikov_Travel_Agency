using TravelAgencyBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        [Required]

        public RequestStatus Status { get; set; }
        [ForeignKey("RequestID")]
        public virtual List<RequestGuide> RequestGuides { get; set; }
        public virtual Supplier Supplier { get; set; }
        public DateTime Date { get; set; }
    }
}
