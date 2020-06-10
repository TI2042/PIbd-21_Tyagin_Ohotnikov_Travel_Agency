using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgencyDatabaseImplement;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml.Serialization;

namespace TravelAgencyDatabaseImplement.Implements
{
    public class TourLogic : ITourLogic
    {
        public void CreateOrUpdate(TourBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Tour element = context.Tours.FirstOrDefault(rec =>
                       rec.TourName == model.TourName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть изделие с таким названием");
                        }
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
                        element.TourName = model.TourName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var TourGuides = context.TourGuides.Where(rec
                           => rec.TourId == model.Id.Value).ToList();
                            // удалили те, которых нет в модели
                            context.TourGuides.RemoveRange(TourGuides.Where(rec =>
                            !model.TourGuides.ContainsKey(rec.GuideId)).ToList());
                            context.SaveChanges();
                            // обновили количество у существующих записей
                            foreach (var updateGuide in TourGuides)
                            {
                                updateGuide.Count =
                               model.TourGuides[updateGuide.GuideId].Item2;

                                model.TourGuides.Remove(updateGuide.GuideId);
                            }
                            context.SaveChanges();
                        }
                        // добавили новые
                        foreach (var pc in model.TourGuides)
                        {
                            context.TourGuides.Add(new TourGuide
                            {
                                TourId = element.Id,
                                GuideId = pc.Key,
                                Count = pc.Value.Item2
                            });
                            context.SaveChanges();
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
        public void Delete(TourBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        
                        context.TourGuides.RemoveRange(context.TourGuides.Where(rec =>
                        rec.TourId == model.Id));
                        Tour element = context.Tours.FirstOrDefault(rec => rec.Id
                        == model.Id);
                        if (element != null)
                        {
                            context.Tours.Remove(element);
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
        public List<TourViewModel> Read(TourBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Tours.Where(rec => model == null || rec.Id == model.Id)
                .ToList()
                .Select(rec => new TourViewModel
                {
                    Id = rec.Id,
                    TourName = rec.TourName,
                    Price = rec.Price,
                    TourGuides = context.TourGuides
                                        .Include(recPC => recPC.Guide)
                                        .Where(recPC => recPC.TourId == rec.Id)
                                        .ToDictionary(recPC => recPC.GuideId, recPC => (
                                            recPC.Guide?.GuideThemeName, recPC.Count
                                         ))
                }).ToList();
            }
        }

        public void SaveJson(string folderName)
        {
            string fileName = $"{folderName}\\tour.json";
            using (var context = new TravelAgencyDatabase())
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Tour>));
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, context.Tours);
                }
            }
        }

        public void SaveXml(string folderName)
        {
            string fileName = $"{folderName}\\tour.xml";
            using (var context = new TravelAgencyDatabase())
            {
                XmlSerializer fomatter = new XmlSerializer(typeof(DbSet<Tour>));
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    fomatter.Serialize(fs, context.Tours);
                }
            }
        }
    }
}