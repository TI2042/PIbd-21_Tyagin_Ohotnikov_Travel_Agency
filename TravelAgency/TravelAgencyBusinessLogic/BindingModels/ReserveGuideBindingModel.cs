using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class ReserveGuideBindingModel
    {
        public int HotelId { get; set; }
        public int GuideId { get; set; }
        public int Count { get; set; }
    }
}
