using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    class TourGuideViewModel
    {
        public int Id { get; set; }
        public int GuideId { get; set; }
        public int TourId { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
