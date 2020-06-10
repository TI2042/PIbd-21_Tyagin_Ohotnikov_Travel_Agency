using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class ReportGuideViewModel
    {
        public string SupplierFIO { get; set; }
        public string GuideThemeName { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}