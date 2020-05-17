using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public int FridgeId { get; set; }
        [Required]
        public string SupplierFIO { get; set; }
        [Required]
        public int Password { get; set; }
        public Fridge Fridge { get; set; }
        [ForeignKey("SupplierId")]
        public virtual List<Request> Requests { get; set; }
    }
}
