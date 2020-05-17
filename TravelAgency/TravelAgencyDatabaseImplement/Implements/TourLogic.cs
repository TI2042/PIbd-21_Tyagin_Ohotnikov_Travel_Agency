using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantDatabaseImplement.Implements
{
    public class TourLogic : ITourLogic
    {
        public void CreateOrUpdate(TourBindingModel model)
        {
            using (var context = new RestaurantDatabase())
            {
                Tour element;
                if (model.Id.HasValue)
                {
                    element = context.Tours.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Tour();
                    context.Tours.Add(element);
                }
                element.GuideId = model.GuideId == 0 ? element.GuideId : model.GuideId;
                element.Count = model.Count;
                element.Sum = model.Sum;
                element.Status = model.Status;
                element.CreationDate = model.CreationDate;
                element.CompletionDate = model.CompletionDate;
                context.SaveChanges();
            }
        }
        public void Delete(TourBindingModel model)
        {
            using (var context = new RestaurantDatabase())
            {
                Tour element = context.Tours.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Tours.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<TourViewModel> Read(TourBindingModel model)
        {
            using (var context = new RestaurantDatabase())
            {
                return context.Tours
            .Include(rec => rec.Guide)
            .Where(
                    rec => model == null
                    || (rec.Id == model.Id && model.Id.HasValue)
                    || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.CreationDate >= model.DateFrom && rec.CreationDate <= model.DateTo))
            .Select(rec => new TourViewModel
            {
                Id = rec.Id,
                GuideName = rec.Guide.GuideName,
                Count = rec.Count,
                Sum = rec.Sum,
                Status = rec.Status,
                CreationDate = rec.CreationDate,
                CompletionDate = rec.CompletionDate
            })
            .ToList();
            }
        }
    }
}