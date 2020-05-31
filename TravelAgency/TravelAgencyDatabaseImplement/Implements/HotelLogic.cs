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
        public List<HotelViewModel> Read(HotelBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Hotels
                .Where(rec => model == null || rec.Id == model.Id
                    || rec.SupplierId == model.SupplierId)
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
                            .ToDictionary(recCC => recCC.GuideId, recCC =>
                            (recCC.Guide?.GuideName, recCC.Count, recCC.Reserved))
                })
                    .ToList();
            }
        }

        public void CreateOrUpdate(HotelBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Hotel element = context.Hotels.FirstOrDefault(rec => rec.HotelName == model.HotelName && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже существует холодильник с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.Hotels.FirstOrDefault(rec => rec.Id == model.Id);
                    int free = context.HotelGuides.Where(rec =>
                    rec.HotelId == model.Id).Sum(rec => rec.Count);
                    int res = context.HotelGuides.Where(rec =>
                    rec.HotelId == model.Id).Sum(rec => rec.Reserved);
                    if ((free + res) > model.Capacity)
                    {
                        throw new Exception("Вместимость не может быть меньше количества продуктов в холодильнике");
                    }
                    if (element == null)
                    {
                        throw new Exception("Холодильник не найден");
                    }
                }
                else
                {
                    element = new Hotel();
                    context.Hotels.Add(element);
                }
                element.SupplierId = model.SupplierId;
                element.HotelName = model.HotelName;
                element.Capacity = model.Capacity;
                element.Country = model.Country;
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
                            throw new Exception("Холодильник не найден");
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
                var hotelGuides = context.HotelGuides.FirstOrDefault(rec =>
                 rec.HotelId == model.HotelId && rec.GuideId == model.GuideId);
                var hotel = context.Hotels.FirstOrDefault(rec => rec.Id == model.HotelId);

                int free = context.HotelGuides.Where(rec =>
                rec.HotelId == model.HotelId).Sum(rec => rec.Count);
                int res = context.HotelGuides.Where(rec =>
                rec.HotelId == model.HotelId).Sum(rec => rec.Reserved);
                if ((free + res + model.Count) > hotel.Capacity)
                {
                    throw new Exception("Недостаточно места в холодильнике");
                }
                if (hotelGuides == null)
                {
                    context.HotelGuides.Add(new HotelGuide
                    {
                        HotelId = model.HotelId,
                        GuideId = model.GuideId,
                        Count = model.Count,
                        Reserved = 0
                    });
                }
                else
                {
                    hotelGuides.Count += model.Count;
                }
                context.SaveChanges();
            }
        }

        public void ReserveGuides(ReserveGuideBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var hotelGuides = context.HotelGuides.FirstOrDefault(rec =>
                rec.HotelId == model.HotelId && rec.GuideId == model.GuideId);
                if (hotelGuides != null)
                {
                    if (hotelGuides.Count >= model.Count)
                    {
                        hotelGuides.Count -= model.Count;
                        hotelGuides.Reserved += model.Count;
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Недостаточно продуктов для резервирования");
                    }
                }
                else
                {
                    throw new Exception("На складе не существует таких продуктов");
                }
            }
        }
    }
}