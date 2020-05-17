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
        [DisplayName("Страна")]
        public string HotelCountry { get; set; }
        [DisplayName("Адрес")]
        public string HotelAdress { get; set; }
    }
}
