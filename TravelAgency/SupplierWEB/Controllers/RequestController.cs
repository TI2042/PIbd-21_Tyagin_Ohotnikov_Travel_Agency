using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.HelperModels;
using TravelAgencyBusinessLogic.Interfaces;

namespace SupplierWEB.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestLogic requestLogic;
        private readonly IHotelLogic hotelLogic;
        private readonly SupplierBusinessLogic supplierLogic;
        private readonly SupplierReportLogic reportLogic;
        public RequestController(IRequestLogic requestLogic, IHotelLogic hotelLogic, SupplierBusinessLogic supplierLogic, SupplierReportLogic reportLogic)
        {
            this.requestLogic = requestLogic;
            this.hotelLogic = hotelLogic;
            this.supplierLogic = supplierLogic;
            this.reportLogic = reportLogic;
        }

        public IActionResult Request()
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            var requests = requestLogic.Read(new RequestBindingModel
            {
                SupplierId = Program.Supplier.Id
            });
            return View(requests);
        }

        public IActionResult RequestView(int ID)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            if (TempData["ErrorGuideReserve"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorGuideReserve"].ToString());
            }
            ViewBag.RequestID = ID;
            var guides = requestLogic.Read(new RequestBindingModel
            {
                Id = ID
            })?[0].Guides;
            return View(guides);
        }

        public IActionResult Reserve(int requestId, int guideId)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            supplierLogic.ReserveGuides(new ReserveGuideBindingModel
            {
                RequestId = requestId,
                GuideId = guideId
            });
            return RedirectToAction("RequestView", new { id = requestId });
        }

        public IActionResult AcceptRequest(int id)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            supplierLogic.AcceptRequest(new ChangeRequestStatusBindingModel
            {
                RequestId = id
            });
            return RedirectToAction("Request");
        }

        public IActionResult CompleteRequest(int id)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            supplierLogic.CompleteRequest(new ChangeRequestStatusBindingModel
            {
                RequestId = id
            });
            return RedirectToAction("Request");
        }

        public IActionResult ListGuideAvailable(int id, int count, string name, int requestId)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            ViewBag.GuideName = name;
            ViewBag.Count = count;
            ViewBag.GuideId = id;
            ViewBag.RequestId = requestId;
            var hotels = hotelLogic.GetHotelAvailable(new RequestGuideBindingModel
            {
                GuideId = id,
                Count = count
            });
            return View(hotels);
        }

        public IActionResult SendWordReport(int id)
        {
            string fileName = "D:\\data\\" + id + ".docx";
            reportLogic.SaveNeedGuideToWordFile(new WordInfo
            {
                FileName = fileName,
                RequestId = id,
                SupplierFIO = Program.Supplier.SupplierFIO
            }, Program.Supplier.Email);
            return RedirectToAction("Request");
        }
        public IActionResult SendExcelReport(int id)
        {
            string fileName = "D:\\data\\" + id + ".xlsx";
            reportLogic.SaveNeedGuideToExcelFile(new ExcelInfo
            {
                FileName = fileName,
                RequestId = id,
                SupplierFIO = Program.Supplier.SupplierFIO
            }, Program.Supplier.Email);
            return RedirectToAction("Request");
        }
    }
}