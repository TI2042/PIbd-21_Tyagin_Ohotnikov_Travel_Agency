using TravelAgencyBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BindingModels
{
    public class Tour
    {
        public int? Id { get; set; }
        public string TourName { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Status Status { get; set; }
    }
}
