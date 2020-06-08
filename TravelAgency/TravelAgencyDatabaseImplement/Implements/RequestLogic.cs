using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                                updGuide.InHotel= model.Guides[updGuide.GuideId].Item3;
                                model.Guides.Remove(updGuide.GuideId);
                            }
                            context.SaveChanges();
                        }
                        else
                        {
                            request = new Request();
                            context.Requests.Add(request);
                        }
                        request.SupplierId = model.SupplierId;
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
            if (model.Status != RequestStatus.Processed)
            {
                using (var context = new TravelAgencyDatabase())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            context.RequestGuides.RemoveRange(context.RequestGuides.Where(rec =>
                            rec.RequestID == model.Id));
                            Request element = context.Requests.FirstOrDefault(rec => rec.Id
                            == model.Id);
                            if (element != null)
                            {
                                context.Requests.Remove(element);
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
            else
            {
                throw new Exception("It isn't possible to delete request. Request is processed");
            }
        }
        public List<RequestViewModel> Read(RequestBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Requests
                    .Include(rec => rec.Supplier)
                    .Where(rec => model == null || rec.Id == model.Id || rec.SupplierId == model.SupplierId)
                    .ToList()
                    .Select(rec => new RequestViewModel
                    {
                        Id = rec.Id,
                        SupplierFIO = rec.Supplier.SupplierFIO,
                        SupplierId = rec.SupplierId,
                        Status = rec.Status,
                        Guides = context.RequestGuides
                            .Include(recRF => recRF.Guide)
                            .Where(recRF => recRF.RequestID == rec.Id)
                            .ToDictionary(recRF => recRF.GuideId, recRF =>
                            (recRF.Guide?.GuideName, recRF.Count, recRF.InHotel))
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
    }
}
