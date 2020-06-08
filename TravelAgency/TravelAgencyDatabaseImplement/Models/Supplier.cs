using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [DisplayName("ФИО")]
        public string SupplierFIO { get; set; }
        [DisplayName("Электронная почта")]
        public string Login { get; set; }
        [DisplayName("Пароль")]
        public string Password { get; set; }
        [ForeignKey("SupplierId")]
        public virtual List<Hotel> Hotels { get; set; }
        [ForeignKey("SupplierId")]
        public virtual List<Request> Requests { get; set; }
    }
}
