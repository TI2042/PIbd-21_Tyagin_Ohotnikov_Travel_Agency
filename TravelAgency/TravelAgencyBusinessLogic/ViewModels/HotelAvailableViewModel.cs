using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class HotelAvailableViewModel
    {
        [DisplayName("Отель")]
        public string HotelName { get; set; }
        public int HotelId { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
