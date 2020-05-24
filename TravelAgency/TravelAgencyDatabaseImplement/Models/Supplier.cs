using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [Required]
        public string SupplierFIO { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public virtual List<Request> Requests { get; set; }
    }
}
