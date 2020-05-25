using System.Collections.Generic;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface IGuideLogic
    {
        List<GuideViewModel> Read(GuideBindingModel model);
        void CreateOrUpdate(GuideBindingModel model);
        void Delete(GuideBindingModel model);
    }
}
