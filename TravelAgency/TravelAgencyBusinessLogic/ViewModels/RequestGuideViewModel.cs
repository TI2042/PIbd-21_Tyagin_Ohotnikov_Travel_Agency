using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class RequestGuideViewModel
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int GuideId { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
