using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface ITourLogic
    {
        List<TourViewModel> Read(TourBindingModel model);
        void CreateOrUpdate(TourBindingModel model);
        void Delete(TourBindingModel model);
    }
}