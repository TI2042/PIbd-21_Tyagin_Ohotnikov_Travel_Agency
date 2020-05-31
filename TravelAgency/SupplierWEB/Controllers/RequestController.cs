using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;

namespace SupplierWEB.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestLogic requestLogic;
        public RequestController(IRequestLogic requestLogic)
        {
            this.requestLogic = requestLogic;
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

        public IActionResult RequestView(int ID)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            ViewBag.RequestID = ID;
            var guides = requestLogic.Read(new RequestBindingModel
            {
                Id = ID
            })?[0].Guides;
            return View(guides);
        }
    }
}