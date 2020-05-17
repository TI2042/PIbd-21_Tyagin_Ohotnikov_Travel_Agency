using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface IOrder
    {
        List<TourViewModel> Read(TourViewModel model);
        void CreateOrUpdate(TourViewModel model);
        void Delete(TourViewModel model);
    }
}