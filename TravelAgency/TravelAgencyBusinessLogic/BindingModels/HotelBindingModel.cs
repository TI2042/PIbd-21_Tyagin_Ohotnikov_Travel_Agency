using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class HotelBindingModel
    {
        public int? Id { get; set; }
        public string HotelName { get; set; }
        public int Capacity { get; set; }
        public string Country { get; set; }
    }
}