using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class RequestGuideBindingModel
    {
        public int? Id { get; set; }
        public int RequestId { get; set; }
        public int GuideId { get; set; }
        public int Count { get; set; }
    }
}
