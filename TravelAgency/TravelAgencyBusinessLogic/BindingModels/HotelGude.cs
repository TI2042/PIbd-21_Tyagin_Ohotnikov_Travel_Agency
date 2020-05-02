using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class HotelGude
    {
        public int? Id { get; set; }
        public int HotelId { get; set; }
        public int GuideId { get; set; }
    }
}
