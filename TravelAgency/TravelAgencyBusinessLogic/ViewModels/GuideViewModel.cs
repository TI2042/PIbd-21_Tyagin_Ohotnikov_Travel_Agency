using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class GuideViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя гида")]
        public string GuideName { get; set; }
        [DisplayName("Страна гида")]
        public string GuideContry { get; set; }
        [DisplayName("Отель гида")]
        public string GuideHotel { get; set; }
    }
}
