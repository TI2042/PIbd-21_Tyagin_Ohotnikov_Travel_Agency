using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyDatabaseImplement.Models;

namespace TravelAgencyDatabaseImplement.Implements
{
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Order element;
                if (model.Id.HasValue)
                {
                    element = context.Orders.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Order();
                    context.Orders.Add(element);
                }
                element.TourId = model.TourId == 0 ? element.TourId : model.TourId;
                element.Count = model.Count;
                element.Sum = model.Sum;
                element.Status = model.Status;
                element.CreationDate = model.CreationDate;
                element.CompletionDate = model.CompletionDate;
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Orders
            .Include(rec => rec.Tour)
            .Where(
                    rec => model == null
                    || (rec.Id == model.Id && model.Id.HasValue)
                    || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.CreationDate >= model.DateFrom && rec.CreationDate <= model.DateTo))
            .Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                TourName = rec.Tour.TourName,
                Count = rec.Count,
                Sum = rec.Tour.Price,
                Status = rec.Status,
                CreationDate = rec.CreationDate,
                CompletionDate = rec.CompletionDate
            })
            .ToList();
            }
        }
    }
}
