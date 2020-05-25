using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<TourViewModel> Tours { get; set; }
        public List<HotelViewModel> Hotels { get; set; }
    }
}
