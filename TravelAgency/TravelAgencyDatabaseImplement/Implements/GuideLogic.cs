using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TravelAgencyDatabaseImplement.Implements
{
    public class GuideLogic : IGuideLogic
    {
        public void CreateOrUpdate(GuideBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                Guide element = context.Guides.FirstOrDefault(rec =>
               rec.GuideThemeName == model.GuideThemeName && rec.Id != model.Id);
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
                element.GuideThemeName = model.GuideThemeName;
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
                    GuideThemeName = rec.GuideThemeName,
                    Price = rec.Price
                })
                .ToList();
            }
        }

        public void SaveJsonGuide(string folderName)
        {
            string fileName = $"{folderName}\\guide.json";
            using (var context = new TravelAgencyDatabase())
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(IEnumerable<Guide>));
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, context.Guides);
                }
            }
        }

        public void SaveXmlGuide(string folderName)
        {
            string fileName = $"{folderName}\\guide.xml";
            using (var context = new TravelAgencyDatabase())
            {
                XmlSerializer fomatter = new XmlSerializer(typeof(DbSet<Guide>));
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    fomatter.Serialize(fs, context.Guides);
                }
            }
        }
    }
}