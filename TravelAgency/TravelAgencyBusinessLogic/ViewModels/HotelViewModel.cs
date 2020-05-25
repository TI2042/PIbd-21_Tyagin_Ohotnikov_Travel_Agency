using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class HotelViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название отеля")]
        public string HotelName { get; set; }
        public int SupplierId { get; set; }
        [DisplayName("Поставщик")]
        public string SupplierName { get; set; }
        [DisplayName("Вместимость")]
        public int Capacity { get; set; }
        [DisplayName("Страна отеля")]
        public string Country { get; set; }
        public Dictionary<int, (string, int)> Guides { get; set; }
    }
}
