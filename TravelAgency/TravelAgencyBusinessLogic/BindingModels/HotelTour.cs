using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class HotelTour
    {
        public int? Id { get; set; }
        public int HotelId { get; set; }
        public int TourId { get; set; }
    }
}
