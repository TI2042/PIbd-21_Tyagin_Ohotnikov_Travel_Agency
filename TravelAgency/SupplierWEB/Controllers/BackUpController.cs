using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.Interfaces;

namespace SupplierWEB.Controllers
{
    public class BackUpController : Controller
    {
        private readonly IRequestLogic _request;
        private readonly IHotelLogic _hotel;
        private readonly ISupplierLogic _supplier;
        private readonly IGuideLogic _guide;
        private readonly SupplierReportLogic _supplierReport;
        public BackUpController(IRequestLogic request, IHotelLogic hotel, ISupplierLogic supplier, IGuideLogic guide, SupplierReportLogic supplierReport)
        {
            _request = request;
            _hotel = hotel;
            _supplier = supplier;
            _guide = guide;
            _supplierReport = supplierReport;
        }
        public IActionResult BackUp()
        {
            return View();
        }
        public IActionResult BackUpToJson()
        {
            string fileName = "C:\\Users\\Игорь\\Desktop\\Reports\\BackupJson";
            if (Directory.Exists(fileName))
            {
                _request.SaveJsonRequest(fileName);
                _request.SaveJsonRequestGuide(fileName);
                _hotel.SaveJsonHotel(fileName);
                _hotel.SaveJsonHotelGuide(fileName);
                _supplier.SaveJsonSupplier(fileName);
                _guide.SaveJsonGuide(fileName);
                _supplierReport.SendMailBackup("tis3.1415@gmail.com", fileName, "Бэкап Json", "json");
                return RedirectToAction("BackUp");
            }
            else
            {
                return RedirectToAction("BackUp");
            }
        }
        public IActionResult BackUpToXml()
        {
            string fileName = "C:\\Users\\Игорь\\Desktop\\Reports\\BackupXml";
            if (Directory.Exists(fileName))
            {
                _request.SaveXmlRequest(fileName);
                _request.SaveXmlRequestGuide(fileName);
                _hotel.SaveXmlHotel(fileName);
                _hotel.SaveXmlHotelGuide(fileName);
                _supplier.SaveXmlSupplier(fileName);
                _guide.SaveXmlGuide(fileName);
                _supplierReport.SendMailBackup("tis3.1415@gmail.com", fileName, "Бэкап Xml", "xml");
                return RedirectToAction("BackUp");
            }
            else
            {
                return RedirectToAction("BackUp");
            }
        }
    }
}
