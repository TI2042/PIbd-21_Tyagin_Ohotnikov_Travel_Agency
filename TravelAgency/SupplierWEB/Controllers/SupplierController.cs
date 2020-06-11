using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SupplierWEB.Models;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyDatabaseImplement.Models;

namespace SupplierWEB.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierLogic supplierLogic;
        private readonly int _passwordMaxLength = 25;

        private readonly int _passwordMinLength = 5;
        public SupplierController(ISupplierLogic supplierLogic)
        {
            this.supplierLogic = supplierLogic;
        }

        public IActionResult Logout()
        {
            Program.Supplier = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel supplier)
        {
            if (String.IsNullOrEmpty(supplier.Login)
                || String.IsNullOrEmpty(supplier.Password))
            {
                return View(supplier);
            }
            var supplierView = supplierLogic.Read(new SupplierBindingModel
            {
                Email = supplier.Login,
                Password = supplier.Password
            }).FirstOrDefault();
            if (supplierView == null)
            {
                ModelState.AddModelError("", "Неверный логин или пароль");
                return View(supplier);
            }
            Program.Supplier = supplierView;
            return RedirectToAction("Request", "Request");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationModel supplier)
        {
            var existSupplier = supplierLogic.Read(new SupplierBindingModel
            {
                Email = supplier.Login
            }).FirstOrDefault();
            if (existSupplier != null)
            {
                ModelState.AddModelError("", "Такая почта уже существует");
                return View(supplier);
            }
            if (!Regex.IsMatch(supplier.Login, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                ModelState.AddModelError("", "Такой почты не существует, проверьте корректность введенных данных.");
                return View(supplier);
            }
            if (supplier.Password.Length > _passwordMaxLength ||
                supplier.Password.Length < _passwordMinLength || 
                !Regex.IsMatch(supplier.Password, 
                @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$")
                )
            {
                ModelState.AddModelError("", $"Пароль не соответсвует требованиям:  длина от {_passwordMinLength} до {_passwordMaxLength} символов ,  буквы, цифры, небуквенные символы. ");
                return View(supplier);
            }
            if (String.IsNullOrEmpty(supplier.SupplierFIO)
            || String.IsNullOrEmpty(supplier.Login)
            || String.IsNullOrEmpty(supplier.Password))
            {
                return View(supplier);
            }
            supplierLogic.CreateOrUpdate(new SupplierBindingModel
            {
                SupplierFIO = supplier.SupplierFIO,
                Email = supplier.Login,
                Password = supplier.Password
            });
            return RedirectToAction("Index", "Home");
        }
    }
}