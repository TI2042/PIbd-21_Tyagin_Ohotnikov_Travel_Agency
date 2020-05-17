using System.Collections.Generic;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface IGuideLogic
    {
        List<GuideViewModel> Read(GuideViewModel model);
        void CreateOrUpdate(GuideViewModel model);
        void Delete(GuideViewModel model);
    }
}
