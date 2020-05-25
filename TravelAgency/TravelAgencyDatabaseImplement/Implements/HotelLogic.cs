using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgencyDatabaseImplement;
using Microsoft.EntityFrameworkCore;

namespace TravelAgencyDatabaseImplement.Implements
{
    public class HotelLogic : IHotelLogic
    {
        public void CreateOrUpdate(HotelBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Hotel element = context.Hotels.FirstOrDefault(rec => rec.HotelName == model.HotelName && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть отель с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.Hotels.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Hotel();
                    context.Hotels.Add(element);
                }
                element.HotelName = model.HotelName;
                context.SaveChanges();
            }
        }

        public void Delete(HotelBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.HotelGuides.RemoveRange(context.HotelGuides.Where(rec => rec.HotelId == model.Id));
                        Hotel element = context.Hotels.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element != null)
                        {
                            context.Hotels.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void AddGuide(ReserveGuideBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var HotelGuide = context.HotelGuides
                    .FirstOrDefault(sm => sm.GuideId == model.GuideId && sm.HotelId == model.HotelId);
                if (HotelGuide != null)
                    HotelGuide.Count += model.Count;
                else
                
                    context.HotelGuides.Add(new HotelGuide()
                    {
                        GuideId = model.GuideId,
                        HotelId = model.HotelId,
                        Count = model.Count
                    });
                
                context.SaveChanges();
            }
        }

        public List<HotelViewModel> Read(HotelBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Hotels
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
                .Select(rec => new HotelViewModel
                {
                    Id = rec.Id,
                    HotelName = rec.HotelName,
                    Capacity = rec.Capacity,
                    Country = rec.Country,
                    Guides = context.HotelGuides
                    .Include(recCC => recCC.Guide)
                    .Where(recCC => recCC.HotelId == rec.Id)
                    .ToDictionary(recCC => recCC.GuideId, recCC => (
                    recCC.Guide?.GuideName, recCC.Count))
                })
                .ToList();
            }
        }

        public void RemoveGuides(OrderViewModel order)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var tourGuides = context.TourGuides.Where(dm => dm.TourId == order.TourId).ToList();
                        var HotelGuides = context.TourGuides.ToList();
                        foreach (var guide in tourGuides)
                        {
                            var guideCount = guide.Count * order.Count;
                            foreach(var df in tourGuides)
                            {
                                if (df.GuideId ==guide.GuideId && df.Count>= guideCount)
                                {
                                    df.Count -= guideCount;
                                    guideCount = 0;
                                    break;
                                }
                                else if(df.GuideId==guide.GuideId && df.Count < guideCount)
                                {
                                    guideCount -= df.Count ;
                                    df.Count = 0;
                                    context.SaveChanges();
                                }
                            }
                            if (guideCount > 0)
                            {
                                throw new Exception("В отелях недостаточно гидов ");
                            }
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}