using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;

namespace SupplierWEB.Controllers
{
    public class GuideController : Controller
    {
        private readonly IGuideLogic guideLogic;

        public GuideController(IGuideLogic guideLogic)
        {
            this.guideLogic = guideLogic;
        }

        public IActionResult ListGuides(int hotelId)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            ViewBag.hotelId = hotelId;
            return View(guideLogic.Read(null));
        }

        public IActionResult AddGuide(int? GuideId, int? HotelId)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            if (GuideId == null && HotelId == null)
            {
                return NotFound();
            }
            if (TempData["ErrorLackInHotel"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorLackInHotel"].ToString());
            }
            var guide = guideLogic.Read(new GuideBindingModel
            {
                Id = GuideId
            })?[0];
            if (guide == null)
            {
                return NotFound();
            }
            ViewBag.GuideName = guide.GuideName;
            ViewBag.HotelId = HotelId;
            return View(new RequestGuideBindingModel
            {
                GuideId = GuideId.Value,
                HotelId = HotelId.Value,
            });
        }
    }
}