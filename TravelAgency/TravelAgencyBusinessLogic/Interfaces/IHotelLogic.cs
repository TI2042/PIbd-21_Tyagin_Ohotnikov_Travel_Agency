using System.Collections.Generic;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface IHotelLogic
    {
        List<HotelViewModel> Read(HotelBindingModel model);
        void CreateOrUpdate(HotelBindingModel model);
        void Delete(HotelBindingModel model);
        void AddGuide(ReserveGuideBindingModel model);
        void RemoveGuides(OrderViewModel model);
    }
}
