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
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

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
                            (recCC.Guide?.GuideThemeName, recCC.Count, recCC.Reserved))
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
                    throw new Exception("Уже существует отель с таким названием");
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
                        throw new Exception("Вместимость не может быть меньше количества гидов в отеле");
                    }
                    if (element == null)
                    {
                        throw new Exception("Отель не найден");
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
                            throw new Exception("Отель не найден");
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

        public List<HotelAvailableViewModel> GetHotelAvailable(RequestGuideBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.HotelGuides
                .Include(rec => rec.Hotel)
                .Where(rec => rec.GuideId == model.GuideId
                && rec.Count >= model.Count)
                .Select(rec => new HotelAvailableViewModel
                {
                    HotelId = rec.HotelId,
                    HotelName = rec.Hotel.HotelName,
                    Count = rec.Count
                })
                .ToList();
            }
        }

        public void AddGuide(RequestGuideBindingModel model)
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
                    throw new Exception("Недостаточно места в Отеле");
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

        public void ReserveGuides(RequestGuideBindingModel model)
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
                        throw new Exception("Недостаточно гидов для резервирования");
                    }
                }
                else
                {
                    throw new Exception("В отеле не существует таких гидов");
                }
            }
        }

        public void SaveJsonHotel(string folderName)
        {
            string fileName = $"{folderName}\\Hotel.json";
            using (var context = new TravelAgencyDatabase())
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(IEnumerable<Hotel>));
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, context.Hotels);
                }
            }
        }

        public void SaveJsonHotelGuide(string folderName)
        {
            string fileName = $"{folderName}\\HotelGuide.json";
            using (var context = new TravelAgencyDatabase())
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(IEnumerable<HotelGuide>));
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, context.HotelGuides);
                }
            }
        }

        public void SaveXmlHotel(string folderName)
        {
            string fileNameDop = $"{folderName}\\Hotel.xml";
            using (var context = new TravelAgencyDatabase())
            {
                XmlSerializer fomatterXml = new XmlSerializer(typeof(DbSet<Hotel>));
                using (FileStream fs = new FileStream(fileNameDop, FileMode.Create))
                {
                    fomatterXml.Serialize(fs, context.Hotels);
                }
            }
        }

        public void SaveXmlHotelGuide(string folderName)
        {
            string fileNameDop = $"{folderName}\\HotelGuide.xml";
            using (var context = new TravelAgencyDatabase())
            {
                XmlSerializer fomatterXml = new XmlSerializer(typeof(DbSet<HotelGuide>));
                using (FileStream fs = new FileStream(fileNameDop, FileMode.Create))
                {
                    fomatterXml.Serialize(fs, context.HotelGuides);
                }
            }
        }
    }
}