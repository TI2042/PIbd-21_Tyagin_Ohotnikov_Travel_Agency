using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportGuideViewModel> Guides { get; set; }
    }
}
