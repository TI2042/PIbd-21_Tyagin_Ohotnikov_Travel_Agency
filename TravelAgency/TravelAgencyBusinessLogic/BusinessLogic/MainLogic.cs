using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Enums;
using TravelAgencyBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class MainLogic
    {
        private readonly IOrderLogic orderLogic;
        private readonly IRequestLogic requestLogic;

        public MainLogic(IOrderLogic orderLogic, IRequestLogic requestLogic)
        {
            this.orderLogic = orderLogic;
            this.requestLogic = requestLogic;
        }

        public void CreateOrder(OrderBindingModel model)
        {
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                TourId = model.TourId,
                Count = model.Count,
                CreationDate = DateTime.Now,
                Status = Status.Принят
            });
        }

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel { Id = model.OrderId })?[0];

            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }

            if (order.Status != Status.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }

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
                Status = RequestStatus.Created,
                Guides = model.Guides
            });
        }
    }
}