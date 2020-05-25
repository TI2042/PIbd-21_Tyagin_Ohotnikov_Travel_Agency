using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyDatabaseImplement.Models;
using TravelAgencyDatabaseImplement;
using System.Linq;

namespace TravelAgencyDatabaseImplement.Implements
{
    public class SupplierLogic : ISupplierLogic
    {
        public void CreateOrUpdate(SupplierBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Supplier element = context.Suppliers.FirstOrDefault(rec => rec.SupplierFIO == model.SupplierFIO && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть поставщик с таким логином");
                }
                if (model.Id.HasValue)
                {
                    element = context.Suppliers.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Supplier();
                    context.Suppliers.Add(element);
                }
                element.SupplierFIO = model.SupplierFIO;
                element.Password = model.Password;
                element.Email = model.Email;
                context.SaveChanges();
            }
        }

        public void Delete(SupplierBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Supplier element = context.Suppliers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Suppliers.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<SupplierViewModel> Read(SupplierBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Suppliers
                .Where(rec => (rec.SupplierFIO == model.SupplierFIO) && (model.Password == null || rec.Password == model.Password))
                .Select(rec => new SupplierViewModel
                {
                    Id = rec.Id,
                    SupplierFIO = rec.SupplierFIO,
                    Password = rec.Password,
                    Email = rec.Email
                })
                .ToList();
            }
        }
    }
}