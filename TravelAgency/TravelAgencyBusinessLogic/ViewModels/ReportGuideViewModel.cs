using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.ViewModels
{
    public class ReportGuideViewModel
    {
        public int RequestId { get; set; }
        public string SupplierFIO { get; set; }
        public string GuideThemeName { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public decimal Price { get; set; }
        public decimal Sum { get; set; }
    }
}