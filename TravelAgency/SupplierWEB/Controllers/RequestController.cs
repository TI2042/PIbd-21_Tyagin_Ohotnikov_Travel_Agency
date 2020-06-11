using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SupplierWEB.Models;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.HelperModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;

namespace SupplierWEB.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestLogic requestLogic;
        private readonly IHotelLogic hotelLogic;
        private readonly SupplierBusinessLogic supplierLogic;
        private readonly SupplierReportLogic reportLogic;
        private readonly IGuideLogic guideLogic;
        public RequestController(IRequestLogic requestLogic, IHotelLogic hotelLogic, SupplierBusinessLogic supplierLogic, SupplierReportLogic reportLogic, IGuideLogic guideLogic)
        {
            this.requestLogic = requestLogic;
            this.hotelLogic = hotelLogic;
            this.supplierLogic = supplierLogic;
            this.reportLogic = reportLogic;
            this.guideLogic = guideLogic;
        }

        public IActionResult Request()
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            var кequests = requestLogic.Read(new RequestBindingModel
            {
                SupplierId = Program.Supplier.Id
            });
            return View(кequests);
        }

        public IActionResult Report(ReportModel model)
        {
            var requests = new List<RequestViewModel>();
            requests = requestLogic.Read(new RequestBindingModel
            {
                SupplierId = Program.Supplier.Id,
                DateFrom = model.From,
                DateTo = model.To
            });
            ViewBag.Requests = requests;
            string fileName = "C:\\Users\\Денис\\Desktop\\Reportpdf.pdf";
            if (model.SendMail)
            {
                reportLogic.SaveGuidesToPdfFile(fileName, new RequestBindingModel
                {
                    SupplierId = Program.Supplier.Id,
                    DateFrom = model.From,
                    DateTo = model.To
                }, Program.Supplier.Email);
            }
            return View();
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
            ViewBag.GuideThemeName = name;
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
            string fileName = "C:\\Users\\Игорь\\Desktop\\Reports\\" + id + ".docx";
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
            string fileName = "C:\\Users\\Игорь\\Desktop\\Reports\\" + id + ".xlsx";
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