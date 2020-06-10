using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Enums;
using TravelAgencyBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class MainLogic
    {
        private readonly IOrderLogic orderLogic;
        private readonly IRequestLogic requestLogic;
        private readonly ITourLogic tourLogic;

        public MainLogic(IOrderLogic orderLogic, IRequestLogic requestLogic, ITourLogic tourLogic)
        {
            this.orderLogic = orderLogic;
            this.requestLogic = requestLogic;
            this.tourLogic = tourLogic;
        }

        public void CreateOrder(OrderBindingModel order)
        {
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                TourId = order.TourId,
                Count = order.Count,
                CreationDate = DateTime.Now,
                Status = Status.Принят
            });
        }

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            var request = requestLogic.Read(new RequestBindingModel { Id = model.OrderId })?[0];

            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }

            if (order.Status != Status.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }

            if (request.Status != RequestStatus.Готова)
            {
                throw new Exception("Гиды ещё не доставлены");
            }

            requestLogic.CreateOrUpdate(new RequestBindingModel
            {
                Status = RequestStatus.Обработана
            });

            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                TourId = order.TourId,
                Count = order.Count,
                Sum = order.Sum,
                CreationDate = order.CreationDate,
                Status = Status.Выполняется
            });
        }

        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != Status.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                TourId = order.TourId,
                Count = order.Count,
                Sum = order.Sum,
                CreationDate = order.CreationDate,
                CompletionDate = DateTime.Now,
                Status = Status.Готов
            });
        }

        public void PayOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != Status.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                TourId = order.TourId,
                Count = order.Count,
                Sum = order.Sum,
                CreationDate = order.CreationDate,
                CompletionDate = order.CompletionDate,
                Status = Status.Оплачен
            });
        }

        public void CreateOrUpdateRequest(RequestBindingModel model)
        {
            requestLogic.CreateOrUpdate(new RequestBindingModel
            {
                Id = model.Id,
                SupplierId = model.SupplierId,
                Status = RequestStatus.Создана,
                Guides = model.Guides
            });
        }

        public List<ReportTourGuideViewModel> GetTourGuidesOrder()
        {
            var tours = tourLogic.Read(null);
            var list = new List<ReportTourGuideViewModel>();
            foreach (var tour in tours)
            {
                foreach (var pc in tour.TourGuides)
                {
                    var record = new ReportTourGuideViewModel
                    {
                        TourName = tour.TourName,
                        GuideThemeName = pc.Value.Item1,
                        Count = pc.Value.Item2
                    };
                    list.Add(record);
                }
            }
            return list;
        }
    }
}