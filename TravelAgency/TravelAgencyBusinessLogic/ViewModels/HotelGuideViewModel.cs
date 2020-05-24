﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class HotelGuideViewModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int GuideId { get; set; }
        [DisplayName("Имя гида")]
        public string GuideName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DisplayName("Резервация")]
        public int IsReserved { get; set; }
    }
}
