using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Enums;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyDatabaseImplement.Models;

namespace TravelAgencyDatabaseImplement.Implements
{
    public class RequestLogic : IRequestLogic
    {
        public void CreateOrUpdate(RequestBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Request request;
                        if (model.Id.HasValue)
                        {
                            request = context.Requests.FirstOrDefault(rec => rec.Id == model.Id);
                            if (request == null)
                            {
                                throw new Exception("Заявка не найдена");
                            }
                            var requestGuides = context.RequestGuides
                                .Where(rec => rec.RequestID == model.Id.Value).ToList();
                            context.RequestGuides.RemoveRange(requestGuides.Where(rec =>
                                !model.Guides.ContainsKey(rec.GuideId)).ToList());
                            foreach (var updGuide in requestGuides)
                            {
                                updGuide.Count = model.Guides[updGuide.GuideId].Item2;
                                updGuide.InHotel = model.Guides[updGuide.GuideId].Item3;
                                model.Guides.Remove(updGuide.GuideId);
                            }
                            request.CompletionDate = model.CompletionDate;
                            context.SaveChanges();
                        }
                        else
                        {
                            request = new Request();
                            context.Requests.Add(request);
                        }
                        request.SupplierId = model.SupplierId;
                        request.Sum = model.Sum;
                        request.Status = model.Status;
                        context.SaveChanges();
                        foreach (var Guide in model.Guides)
                        {
                            context.RequestGuides.Add(new RequestGuide
                            {
                                RequestID = request.Id,
                                GuideId = Guide.Key,
                                Count = Guide.Value.Item2,
                                InHotel = false
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

        public void Delete(RequestBindingModel model)
        {
            if (model.Status != RequestStatus.Выполняется)
            {
                using (var context = new TravelAgencyDatabase())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.RequestGuides.RemoveRange(context
                                .RequestGuides.Where(rec => rec.RequestID == model.Id));
                            Request request = context.Requests.FirstOrDefault(rec => rec.Id == model.Id);
                            if (request != null)
                            {
                                context.Requests.Remove(request);
                                context.SaveChanges();
                            }
                            else
                            {
                                throw new Exception("Заявка не найдена");
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
            else
            {
                throw new Exception("Заявку невозможно удалить. Заявка в процессе");
            }
        }

        public List<RequestViewModel> Read(RequestBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Requests
                    .Include(rec => rec.Supplier)
                    .Where(rec => model == null || rec.Id == model.Id || (rec.SupplierId == model.SupplierId) && (model.DateFrom == null && model.DateTo == null ||
                    rec.CompletionDate >= model.DateFrom && rec.CompletionDate <= model.DateTo && rec.Status == RequestStatus.Готова))
                    .ToList()
                    .Select(rec => new RequestViewModel
                    {
                        Id = rec.Id,
                        SupplierFIO = rec.Supplier.SupplierFIO,
                        SupplierId = rec.SupplierId,
                        Date = rec.CompletionDate,
                        Status = rec.Status,
                        Guides = context.RequestGuides
                            .Include(recRF => recRF.Guide)
                            .Where(recRF => recRF.RequestID == rec.Id)
                            .ToDictionary(recRF => recRF.GuideId, recRF=>
                            (recRF.Guide?.GuideThemeName, recRF.Count, recRF.InHotel)),
                        Sum = Decimal.Round(context.RequestGuides
                            .Include(recRF => recRF.Guide)
                            .Where(recRF => recRF.RequestID == rec.Id)
                            .Sum(recRF => recRF.Guide.Price * recRF.Count), 2)
                    })
                    .ToList();
            }
        }
        public void Reserve(ReserveGuideBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var requestGuides = context.RequestGuides.FirstOrDefault(rec =>
                rec.RequestID == model.RequestId && rec.GuideId == model.GuideId);
                if (requestGuides == null)
                {
                    throw new Exception("Гида нет в заявке");
                }
                requestGuides.InHotel = true;
                context.SaveChanges();
            }
        }

        public void SaveJsonRequest(string folderName)
        {
            string fileName = $"{folderName}\\Request.json";
            using (var context = new TravelAgencyDatabase())
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(IEnumerable<Request>));
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, context.Requests);
                }
            }
        }

        public void SaveJsonRequestGuide(string folderName)
        {
            string fileName = $"{folderName}\\RequestGuide.json";
            using (var context = new TravelAgencyDatabase())
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(IEnumerable<RequestGuide>));
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, context.RequestGuides);
                }
            }
        }

        public void SaveXmlRequest(string folderName)
        {
            string fileNameDop = $"{folderName}\\Request.xml";
            using (var context = new TravelAgencyDatabase())
            {
                XmlSerializer fomatterXml = new XmlSerializer(typeof(DbSet<Request>));
                using (FileStream fs = new FileStream(fileNameDop, FileMode.Create))
                {
                    fomatterXml.Serialize(fs, context.Requests);
                }
            }
        }

        public void SaveXmlRequestGuide(string folderName)
        {
            string fileNameDop = $"{folderName}\\RequestGuide.xml";
            using (var context = new TravelAgencyDatabase())
            {
                XmlSerializer fomatterXml = new XmlSerializer(typeof(DbSet<RequestGuide>));
                using (FileStream fs = new FileStream(fileNameDop, FileMode.Create))
                {
                    fomatterXml.Serialize(fs, context.RequestGuides);
                }
            }
        }
    }
}