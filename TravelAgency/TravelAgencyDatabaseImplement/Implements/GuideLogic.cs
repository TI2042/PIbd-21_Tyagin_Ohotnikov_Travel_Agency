using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelAgencyDatabaseImplement.Implements
{
    public class GuideLogic : IGuideLogic
    {
        public void CreateOrUpdate(GuideBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Guide element = context.Guides.FirstOrDefault(rec =>
               rec.GuideName == model.GuideName && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть гид с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.Guides.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Guide();
                    context.Guides.Add(element);
                }
                element.GuideName = model.GuideName;
                element.Price = model.Price;
                context.SaveChanges();
            }
        }
        public void Delete(GuideBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Guide element = context.Guides.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Guides.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<GuideViewModel> Read(GuideBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Guides
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new GuideViewModel
                {
                    Id = rec.Id,
                    GuideName = rec.GuideName,
                    Price = rec.Price
                })
                .ToList();
            }
        }
    }
}