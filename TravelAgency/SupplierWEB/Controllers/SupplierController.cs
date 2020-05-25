using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SupplierWEB.Models;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;

namespace SupplierWEB.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierLogic _supplier;

        public SupplierController(ISupplierLogic supplier)
        {
            _supplier = supplier;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel supplier)
        {
            var supplierView = _supplier.Read(new SupplierBindingModel
            {
                SupplierFIO = supplier.SupplierFIO,
                Password = supplier.Password
            }).FirstOrDefault();
            if (supplierView == null)
            {
                ModelState.AddModelError("", "Вы ввели неверный пароль, либо пользователь не найден");
                return View(supplier);
            }
            Program.Supplier = supplierView;
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            Program.Supplier = null;
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Registration(RegistrationModel supplier)
        {
            if (ModelState.IsValid)
            {
                var existSupplier = _supplier.Read(new SupplierBindingModel
                {
                    SupplierFIO = supplier.SupplierFIO
                }).FirstOrDefault();
                if (existSupplier != null)
                {
                    ModelState.AddModelError("", "Данный логин уже занят");
                    return View(supplier);
                }
                existSupplier = _supplier.Read(new SupplierBindingModel
                {
                    Email = supplier.Email
                }).FirstOrDefault();
                if (existSupplier != null)
                {
                    ModelState.AddModelError("", "Данный E-Mail уже занят");
                    return View(supplier);
                }
                _supplier.CreateOrUpdate(new SupplierBindingModel
                {
                    SupplierFIO = supplier.SupplierFIO,
                    Password = supplier.Password,
                    Email = supplier.Email
                });
                ModelState.AddModelError("", "Вы успешно зарегистрированы");
                return View("Registration", supplier);
            }
            return View(supplier);
        }
    }
}