using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;
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
                Sum = rec.Sum,
                Status = rec.Status,
                CreationDate = rec.CreationDate,
                CompletionDate = rec.CompletionDate
            })
            .ToList();
            }
        }
        public void SaveJsonOrder(string folderName)
        {
            string fileName = $"{folderName}\\Order.json";
            using (var context = new TravelAgencyDatabase())
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Order>));
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, context.Orders);
                }
            }
        }

        public void SaveXmlOrder(string folderName)
        {
            string fileName = $"{folderName}\\Order.xml";
            using (var context = new TravelAgencyDatabase())
            {
                XmlSerializer fomatter = new XmlSerializer(typeof(DbSet<Order>));
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    fomatter.Serialize(fs, context.Orders);
                }
            }
        }
    }
}
