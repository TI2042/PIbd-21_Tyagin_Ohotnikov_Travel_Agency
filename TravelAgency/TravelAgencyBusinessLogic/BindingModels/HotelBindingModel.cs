using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class Hotel
    {
        public int? Id { get; set; }
        public string HotelName { get; set; }
        public string Adress { get; set; }
        public string Country { get; set; }
    }
}