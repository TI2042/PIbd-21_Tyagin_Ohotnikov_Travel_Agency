using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class RequestBindingModel
    {
        public int? Id { get; set; }
        public int? SupplierId { get; set; }
        public decimal Sum { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public Status Status { get; set; }
        public List<RequestGuideBindingModel> RequestGuides { get; set; }
    }
}
