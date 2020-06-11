using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните поле")]
        [DisplayName("Название")]
        public string HotelName { get; set; }
        [Required(ErrorMessage = "Заполните поле")]
        [DisplayName("Вместимость")]
        public int Capacity { get; set; }
        [Required(ErrorMessage = "Заполните поле")]
        [DisplayName("Страна")]
        public string Country { get; set; }
        [ForeignKey("HotelId")]
        public virtual List<HotelGuide> HotelGuides { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}