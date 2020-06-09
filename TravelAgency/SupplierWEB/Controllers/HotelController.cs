using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyDatabaseImplement.Models;

namespace SupplierWEB.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelLogic hotelLogic;
        private readonly IGuideLogic guideLogic;

        public HotelController(IHotelLogic hotelLogic, IGuideLogic guideLogic)
        {
            this.hotelLogic = hotelLogic;
            this.guideLogic = guideLogic;
        }

        public IActionResult ListHotels()
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            var hotel = hotelLogic.Read(new HotelBindingModel
            {
                SupplierId = Program.Supplier.Id
            });
            return View(hotel);
        }

        public IActionResult Details(int? id)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.HotelId = id;
            var guides = hotelLogic.Read(new HotelBindingModel
            {
                Id = id
            })?[0].Guides;
            return View(guides);
        }

        public IActionResult CreateHotel()
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateHotel([Bind("HotelName,Capacity,Type")] Hotel hotel)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    hotelLogic.CreateOrUpdate(new HotelBindingModel
                    {
                        HotelName = hotel.HotelName,
                        Capacity = hotel.Capacity,
                        Country = hotel.Country,
                        SupplierId = Program.Supplier.Id
                    });
                    return RedirectToAction(nameof(ListHotels));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(hotel);
        }

        public IActionResult ChangeHotel(int? id)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            if (id == null)
            {
                return NotFound();
            }
            var hotel = hotelLogic.Read(new HotelBindingModel
            {
                Id = id
            })?[0];
            if (hotel == null)
            {
                return NotFound();
            }
            return View(new Hotel
            {
                Id = id.Value,
                HotelName = hotel.HotelName,
                Capacity = hotel.Capacity,
                Country = hotel.Country
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeHotel(int id, [Bind("Id,HotelName,Capacity,Type")] Hotel hotel)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            if (id != hotel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    hotelLogic.CreateOrUpdate(new HotelBindingModel
                    {
                        Id = id,
                        HotelName = hotel.HotelName,
                        Capacity = hotel.Capacity,
                        Country = hotel.Country,
                        SupplierId = Program.Supplier.Id
                    });
                }
                catch (Exception exception)
                {
                    if (!HotelExists(hotel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", exception.Message);
                        return View(hotel);
                    }
                }
                return RedirectToAction(nameof(ListHotels));
            }
            return View(hotel);
        }

        public IActionResult DeleteHotel(int? id)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            if (id == null)
            {
                return NotFound();
            }
            var hotel = hotelLogic.Read(new HotelBindingModel
            {
                Id = id
            })?[0];
            if (hotel == null)
            {
                return NotFound();
            }
            return View(new Hotel
            {
                Id = id.Value,
                HotelName = hotel.HotelName,
                Capacity = hotel.Capacity,
                Country = hotel.Country
            });
        }

        [HttpPost, ActionName("DeleteHotel")]
        [ValidateAntiForgeryToken]
        public IActionResult Completion(int id)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            hotelLogic.Delete(new HotelBindingModel
            {
                Id = id
            });
            return RedirectToAction(nameof(ListHotels));
        }

        private bool HotelExists(int id)
        {
            return hotelLogic.Read(new HotelBindingModel
            {
                Id = id
            }).Count == 1;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddGuide([Bind("HotelId, GuideId, Count")] RequestGuideBindingModel model)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    hotelLogic.AddGuide(model);
                }
                catch (Exception exception)
                {
                    TempData["ErrorLackInHotel"] = exception.Message;
                    return RedirectToAction("AddGuide", "Guide", new
                    {
                        guideId = model.GuideId,
                        hotelId = model.HotelId
                    });
                }
            }
            return RedirectToAction("Details", new { id = model.HotelId });
        }

        public IActionResult ReserveGuides(int hotelId, int guideId, int count, int requestId)
        {
            if (Program.Supplier == null)
            {
                return new UnauthorizedResult();
            }
            try
            {
                hotelLogic.ReserveGuides(new RequestGuideBindingModel
                {
                    HotelId = hotelId,
                    GuideId = guideId,
                    Count = count
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorGuideReserve"] = ex.Message;
                return RedirectToAction("RequestView", "Request", new { id = requestId });
            }
            return RedirectToAction("Reserve", "Request", new
            {
                RequestId = requestId,
                GuideId = guideId
            });
        }
    }
}