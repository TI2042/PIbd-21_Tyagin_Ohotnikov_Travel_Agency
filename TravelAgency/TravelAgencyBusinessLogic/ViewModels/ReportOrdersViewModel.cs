using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class ReportOrdersViewModel
    {
        public DateTime CreationDate { get; set; }
        public string TourName { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }
        public Status Status { get; set; }
    }
}
