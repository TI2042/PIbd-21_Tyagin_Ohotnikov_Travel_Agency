using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportOrdersViewModel> Orders { get; set; }
        public List<HotelViewModel> Hotels { get; set; }
        public int RequestId { get; set; }
        public string SupplierFIO { get; set; }
        public DateTime DateComplete { get; set; }
        public Dictionary<int, (string, int, bool)> RequestGuides { get; set; }
        public List<ReportTourGuideViewModel> TourGuides { get; set; }
    }
}
