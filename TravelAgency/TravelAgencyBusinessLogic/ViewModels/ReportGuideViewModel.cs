using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class ReportGuideViewModel
    {
        public string GuideName { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Price { get; set; }
    }
}