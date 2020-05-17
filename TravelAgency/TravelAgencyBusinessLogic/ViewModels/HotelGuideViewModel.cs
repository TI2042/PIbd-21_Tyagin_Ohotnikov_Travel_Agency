using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class HotelGuideViewModel
    {
        public int Id { get; set; }
        public int Guide { get; set; }
        public int HotelId { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
