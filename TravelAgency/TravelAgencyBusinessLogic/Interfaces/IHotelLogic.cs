using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface IHotelLogic
    {
        List<HotelViewModel> Read(HotelViewModel model);
        void CreateOrUpdate(HotelViewModel model);
        void Delete(HotelViewModel model);
    }
}
